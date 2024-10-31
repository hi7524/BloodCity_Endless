using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
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
    public float dis = 0; // 플레이어와의 거리

    [HideInInspector]
    public Vector2 dir = Vector2.zero; // 플레이어의 방향 

    [HideInInspector]
    public int hp; // 현재 체력

    [HideInInspector]
    public bool isInstantSpawn = false; // 간이 소환 여부 

    [HideInInspector]
    public bool isDead; // 사망 여부

    [SerializeField]
    private GameObject Exp; // 경험치 오브젝트

    [SerializeField]
    private GameObject Coin; // 코인 오브젝트


    protected IMobSkill[] mobSkills; // 스킬 배열

    private SpriteRenderer render; // 스프라이트 렌더러
    private int bodyAttackTime = 1; // 몸빵 피해 대기 시간
    private bool isCanbodyAttack = true; // 몸빵 피해 가능 여부 

    private void Start()
    {

        Mob = gameObject;

        Target = GameObject.FindWithTag("Player"); // 생성 시 즉시 설정

        RB = GetComponent<Rigidbody2D>();

        mobSkills = GetComponents<IMobSkill>(); // 스킬 스크립트 컴포넌트

        hp = Random.Range(obj.minHealth, obj.maxHealth + 1); // 현재 체력 초기화

        foreach (IMobSkill skill in mobSkills) // 스킬 초기화
        {

            skill.Init();

            if (skill.data.skillTag == MobSkillTag.Init) // 초기화형 스킬일 경우 즉시 사용
                skill.Use(this);

        }


        // 스프라이트 및 애니메이션 설정
        render = GetComponent<SpriteRenderer>();

        if (obj.AnimeControllers.Length > 0)
            GetComponent<Animator>().runtimeAnimatorController = obj.AnimeControllers[Random.Range(0, obj.AnimeControllers.Length)];

    }

    private void Update()
    {

        if (!isDead) // 사망 상태가 아닌 경우에만
        {
            dis = Vector2.Distance(transform.position, Target.transform.position); // 거리 차 계산
            dir = (Target.transform.position - transform.position).normalized; // 방향 계산
            
            Routine_Move(dis, dir);
            Routine_Attack(dis, dir);
        }

    }

    private void Routine_Move(float dis, Vector2 dir) // 이동 루틴
    {

        if(!isUsingSkillState) // 스킬 사용 상태가 아닌 경우에만
        {

            render.flipX = dir.x > 0; // 스프라이트 플립 변경

            if (obj.MoveType == AI_MoveType.Normal) // 단순 추적
            {

                RB.MovePosition(RB.position + dir * (1 + (obj.speed * 0.1f)) * Time.deltaTime); // 플레이어 방향으로 이동

            }
            else if (obj.MoveType == AI_MoveType.Distance) // 거리 조절
            {

                if(obj.Distance_Range > 0) // 추적
                {

                    if ((obj.Distance_Range / 20) < dis) // 거리 범위 밖이라면 접근
                    {

                        RB.MovePosition(RB.position + dir * (1 + (obj.speed * 0.1f)) * Time.deltaTime); // 플레이어 방향으로 이동

                    }

                }
                else // 도주
                {

                    if ((obj.Distance_Range * -1 / 20) >= dis) // 거리 범위 안이라면 도주
                    {

                        RB.MovePosition(RB.position - dir * (1 + (obj.speed * 0.1f)) * Time.deltaTime); // 플레이어 방향으로 이동

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

        foreach(IMobSkill skill in mobSkills)
        {

            if (!skill.coolDown && skill.data && skill.data.skillTag != MobSkillTag.Dead)
            {

                skill.coolDown = true;

                skill.Use(this);

                StartCoroutine(Skill_Cooltime(skill));

            }

        }

    }

    private IEnumerator Skill_Cooltime(IMobSkill skill) // 스킬 쿨타임
    {

        yield return new WaitForSeconds(skill.data.coolTime); // 대기

        skill.coolDown = false;

    }


    public void Damaged(int damage) // 몬스터 피해
    {
        
        if(!isDead) // 사망 상태가 아닌 경우 
        {

            hp -= damage; // 피해

            if(hp <= 0) // 사망할 경우
            {

                // 경험치 드랍
                int expValue = Random.Range(obj.dropExp[0], obj.dropExp[1] + 1);
                if (expValue > 0)
                {
                    GameObject exp = Instantiate(Exp, gameObject.transform.position, gameObject.transform.rotation);
                    exp.GetComponent<Exp>().xpData.point = expValue;
                }

                // 강화 코인 드랍
                int coinPer = Random.Range(1, 101);
                if (obj.upgradeCoinDropPer >= coinPer)
                    Instantiate(Coin, gameObject.transform.position, gameObject.transform.rotation);


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
                {

                    Dead();

                }

            }

        }

    }

    public void Dead() // 몬스터 사망 처리
    {
        if (isInstantSpawn)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {

        GameObject Player = coll.gameObject;

        if (Player.tag == "Player" && isCanbodyAttack && !isDead) // 몸빵 피해 적용
        {

            isCanbodyAttack = false;

            Player.GetComponent<PlayerHealth>().Damaged(obj.attackDamage); // 피해 적용
            
            Invoke("BodyAttack_Delay", bodyAttackTime);

        }

    }

    void BodyAttack_Delay() // 몸빵 피해 대기
    {
        isCanbodyAttack = true;
    }

}
