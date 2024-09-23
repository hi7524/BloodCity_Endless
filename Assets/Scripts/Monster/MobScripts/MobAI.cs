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


    IMobSkill[] mobSkills;

    int bodyAttackTime = 1; // 몸빵 피해 대기 시간
    bool isCanbodyAttack = true; // 몸빵 피해 가능 여부 

    private void Start()
    {

        Mob = gameObject;

        Target = GameObject.FindWithTag("Player"); // 생성 시 즉시 설정

        RB = GetComponent<Rigidbody2D>();

        mobSkills = GetComponents<IMobSkill>(); // 스킬 스크립트 컴포넌트

        foreach (IMobSkill skill in mobSkills)
        {

            skill.Init();

        }

    }

    private void Update()
    {

        dis = Vector2.Distance(transform.position, Target.transform.position); // 거리 차 계산
        dir = (Target.transform.position - transform.position).normalized; // 방향 계산

        Routine_Move(dis, dir);
        Routine_Attack(dis, dir);

    }

    private void Routine_Move(float dis, Vector2 dir) // 이동 루틴
    {

        if(!isUsingSkillState) // 스킬 사용 상태가 아닌 경우에만
        {

            if (obj.MoveType == AI_MoveType.Normal) // 단순 추적
            {

                RB.MovePosition(RB.position + dir * (1 + (obj.speed * 0.15f)) * Time.deltaTime); // 플레이어 방향으로 이동

            }
            else if (obj.MoveType == AI_MoveType.Distance) // 거리 조절
            {

                if (obj.Distance_Range < dis) // 거리 범위 밖이라면 접근
                {

                    RB.MovePosition(RB.position + dir * (1 + (obj.speed * 0.15f)) * Time.deltaTime); // 플레이어 방향으로 이동

                }

            }

        }

    }

    private void Routine_Attack(float dis, Vector2 dir) // 공격 루틴
    {

        if (obj.AttackType == AI_AttackType.Simple)
        {



        }
        else if (obj.AttackType == AI_AttackType.Skill)
        {

            if (obj.Attack_Range >= dis) // 범위 안이라면 스킬 사용
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

    private void OnTriggerStay2D(Collider2D coll)
    {

        GameObject Player = coll.gameObject;

        if (Player.tag == "Player" && isCanbodyAttack) // 몸빵 피해 적용
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
