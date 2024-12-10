using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MobAI : MonoBehaviour
{

    public MobObj obj; // 몬스터 정보 참조용 스크립터블 오브젝트

    [HideInInspector]
    public Rigidbody2D RB; // 몬스터 리지드 바디 

    [HideInInspector]
    public GameObject Mob; // 본인 오브젝트

    [HideInInspector]
    public GameObject Target; // 타겟 플레이어 오브젝트

    [HideInInspector]
    public bool isUsingSkillState = false; // 스킬 사용 상태 여부
    [HideInInspector]
    public bool isSkillBanState = false; // 스킬 차단 상태 여부

    [HideInInspector]
    public float dis = 0; // 플레이어와의 거리

    [HideInInspector]
    public Vector2 dir = Vector2.zero; // 플레이어의 방향 

    //[HideInInspector]
    public int hp; // 현재 체력

    //[HideInInspector]
    public float speed; // 현재 이동속도

    //[HideInInspector]
    public bool isInstantSpawn = false; // 간이 소환 여부 

    [HideInInspector]
    public bool isDead = true; // 사망(활동 중단) 여부

    [SerializeField]
    private GameObject Exp; // 경험치 오브젝트

    [SerializeField]
    private GameObject Coin; // 코인 오브젝트


    protected IMobSkill[] mobSkills; // 스킬 배열

    public SpriteRenderer render; // 스프라이트 렌더러
    public bool isReverseSprite; // 스프라이트 반대 방향 여부

    private Coroutine coroutine; // 무한 루프 코루틴

    public int bodyAttackDamage; // 몸빵 피해량
    private int bodyAttackTime = 1; // 몸빵 피해 대기 시간
    private bool isCanbodyAttack = true; // 몸빵 피해 가능 여부 

    private void Start()
    {
        if (isInstantSpawn)
            Init();
    }

    public void Init(float hpPer = 1) // 초기화
    {
        gameObject.SetActive(true);
    
        isDead = false;

        Mob = gameObject;

        Target = GameObject.FindWithTag("Player"); // 생성 시 즉시 설정

        RB = GetComponent<Rigidbody2D>();

        mobSkills = GetComponents<IMobSkill>(); // 스킬 스크립트 컴포넌트

        hp = (int)(Random.Range(obj.minHealth, obj.maxHealth + 1) * hpPer); // 현재 체력 초기화
        speed = obj.speed; // 현재 이속 초기화
        bodyAttackDamage = obj.attackDamage; // 현재 몸빵 피해량 초기화

        foreach (IMobSkill skill in mobSkills) // 스킬 초기화
        {

            skill.Init();

            if (skill.data.skillTag == MobSkillTag.Init) // 초기화형 스킬일 경우 즉시 사용
                skill.Use(this);
            else if (skill.data.isStartCooldown) // 시작 시 쿨타임 적용 스킬인 경우 쿨다운
                StartCoroutine(Skill_Cooltime(skill));

        }

        // 스프라이트 및 애니메이션 설정
        render = GetComponent<SpriteRenderer>();

        if (obj.AnimeControllers.Length > 0)
            GetComponent<Animator>().runtimeAnimatorController = obj.AnimeControllers[Random.Range(0, obj.AnimeControllers.Length)];

        // 루틴 시작
        coroutine = StartCoroutine(Routine()); // 코루틴 최초 시작

    }

    public void AddSpeed(float value) // 이동 속도 증감
    {
        speed += value;
        speed = speed < 0 ? 0 : speed;
    }

    public void SetSpeed(float value) // 이동 속도 설정
    {
       speed = value;
       speed = speed < 0 ? 0 : speed;
    }

    public void ResetSpeed() // 이동 속도 초기화
    {
        speed = obj.speed;
    }


    private IEnumerator Routine() // 루틴 무한 반복 코루틴
    {
        while (true)
        {
            if (!isDead && Target) // 사망 상태가 아닌 경우에만
            {
                dis = Vector2.Distance(transform.position, Target.transform.position); // 거리 차 계산
                dir = (Target.transform.position - transform.position).normalized; // 방향 계산
    
                Routine_Move(dis, dir);
                Routine_Attack(dis, dir);
            }
            else
                Target = GameObject.FindWithTag("Player");

            // 카메라가 오브젝트를 볼 수 있는지 체크
            if (IsObjectVisible(Camera.main, gameObject))
            {
                // 오브젝트가 보이면 렌더링 활성화
                render.enabled = true;
            }
            else
            {
                // 오브젝트가 보이지 않으면 렌더링 비활성화
                render.enabled = false;
            }
                
            yield return new WaitForSeconds(0.001f); // 0.001초마다 반복 수행
        }
    }

    private void Routine_Move(float dis, Vector2 dir) // 이동 루틴
    {

        float addSpeed = speed * 0.01f * 2.75f;      
        
        if(!isUsingSkillState && speed > 0) // 스킬 사용 상태가 아니고 이동 속도가 1이상일 때만
        {

            render.flipX = (!isReverseSprite ? dir.x > 0 : dir.x < 0); // 스프라이트 플립 변경

            if (obj.MoveType == AI_MoveType.Normal) // 단순 추적
            {
                transform.Translate((Vector3)dir * addSpeed * Time.deltaTime);
                RB.velocity = Vector3.zero; 

            }
            else if (obj.MoveType == AI_MoveType.Distance) // 거리 조절
            {

                if(obj.Distance_Range > 0) // 추적
                {

                    if ((obj.Distance_Range / 20) < dis) // 거리 범위 밖이라면 접근
                    {

                        transform.Translate((Vector3)dir * addSpeed * Time.deltaTime); // 플레이어 방향으로 이동
                        RB.velocity = Vector3.zero;

                    }

                }
                else // 도주
                {

                    if ((obj.Distance_Range * -1 / 20) >= dis) // 거리 범위 안이라면 도주
                    {

                        transform.Translate((Vector3)dir * -addSpeed * Time.deltaTime); // 플레이어 방향으로 이동
                        RB.velocity = Vector3.zero;

                    }
                    else // 거리 범위 밖이라면 추적
                    {

                        transform.Translate((Vector3)dir * addSpeed * Time.deltaTime); // 플레이어 방향으로 이동
                        RB.velocity = Vector3.zero;

                    }

                }

            }

        }

    }

    private void Routine_Attack(float dis, Vector2 dir) // 공격 루틴 (스킬 타입 전용)
    {

         if (obj.AttackType == AI_AttackType.Skill)
        {

            if (obj.Attack_Range >= dis || obj.Attack_Range == 0) // 범위 안이라면 스킬 사용
            {

                Skill_Use();

            }

        }

    }

    private void Skill_Use() // 스킬 사용
    {

        if(!isSkillBanState)
            foreach(IMobSkill skill in mobSkills)
            {

                if (!skill.coolDown && skill.data && skill.data.skillTag != MobSkillTag.Dead)
                {

                    StartCoroutine(Skill_Cooltime(skill));

                    skill.Use(this);

                    if (isSkillBanState || isUsingSkillState)
                        break;

                }

            }

    }

    private IEnumerator Skill_Cooltime(IMobSkill skill) // 스킬 쿨타임
    {

        skill.coolDown = true;

        yield return new WaitForSeconds(skill.data.coolTime); // 대기

        skill.coolDown = false;

    }


    public void Damaged(int damage) // 몬스터 피해
    {
        
        if(!isDead) // 사망 상태가 아닌 경우 
        {

            render.material.DOColor(Color.red, 0.15f).OnComplete(()=> { render.material.DOColor(Color.white, 0.15f); });
            hp -= damage; // 피해

            if(hp <= 0) // 사망할 경우
            {

                // 경험치 드랍
                int expValue = Random.Range(obj.dropExp[0], obj.dropExp[1] + 1);
                if (expValue > 0)
                {
                    GameObject exp = Instantiate(Exp, gameObject.transform.position + new Vector3(Random.Range(-2, 3), Random.Range(-2, 3), 0), gameObject.transform.rotation);
                    exp.GetComponent<Exp>().xpData.point = expValue;
                }

                // 강화 코인 드랍
                int coinPer = Random.Range(1, 101);
                if (obj.upgradeCoinDropPer >= coinPer)
                    Instantiate(Coin, gameObject.transform.position + new Vector3 (Random.Range(-1, 2), Random.Range(-1, 2), 0), gameObject.transform.rotation);


                // 사망 처리 시작
                bool isDying = true;
                isDead = true;

                foreach (IMobSkill skill in mobSkills) // 사망 시 스킬 검사
                {

                    if (skill.data && skill.data.skillTag == MobSkillTag.Dead)
                    {

                        isDying = false;
                        transform.tag = "Untagged";

                        skill.Use(this);

                    }

                }

                if (isDying) // 별도의 예외 없이 사망하는 경우 사망 처리
                    Dead();

            }

        }

    }

    public void Dead() // 몬스터 사망 처리
    {

        //if (isInstantSpawn)
        //    Destroy(gameObject);
        //else
        //{
        //    gameObject.SetActive(false);

        //    if (coroutine != null)
        //    {
        //        StopCoroutine(coroutine);
        //    }
        //}

        StartCoroutine(Deading());
        
        if (!isInstantSpawn)
            TimeManager.Instance.MonsterDead(transform.position);
    }

    private IEnumerator Deading() // 파괴 대기
    {

        GetComponent<Animator>().Play("Dead");
        GetComponent<CapsuleCollider2D>().enabled = false;

        yield return new WaitForSeconds(0.4f); // 대기
        Destroy(gameObject);

    }


    private void OnCollisionEnter2D(Collision2D coll)
    {

        GameObject Player = coll.gameObject;

        if (Player.tag == "Player" && isCanbodyAttack && !isDead) // 몸빵 피해 적용
        {

            if (!Player.GetComponent<PlayerState>().isPlayerDead) // 사망 상태가 아닐 경우에만
            {
                isCanbodyAttack = false;

                Player.GetComponent<PlayerHealth>().Damaged(obj.attackDamage); // 피해 적용

                Invoke("BodyAttack_Delay", bodyAttackTime);
            }
    
        }

        // 충돌로 인한 속도 변화 (힘이 아닌 속도)
        Vector3 velocityChange = RB.velocity;

        // 임펄스 (속도 변화의 크기)가 최대값을 초과하면 제한
        if (velocityChange.magnitude > 5)
        {
            // 속도 제한
            RB.velocity = velocityChange.normalized * 5;
        }

    }

    void BodyAttack_Delay() // 몸빵 피해 대기
    {
        isCanbodyAttack = true;
    }


    // 카메라가 오브젝트를 보는지 체크하는 함수
    bool IsObjectVisible(Camera camera, GameObject obj)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        CapsuleCollider2D collider = obj.GetComponent<CapsuleCollider2D>();
        return GeometryUtility.TestPlanesAABB(planes, collider.bounds);
    }

    void OnDestroy()
    {
        // 객체가 파괴될 때 코루틴 중지
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

    }

}
