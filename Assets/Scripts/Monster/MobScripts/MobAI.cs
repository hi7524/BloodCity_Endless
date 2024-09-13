using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;


public class MobAI : MonoBehaviour
{

    public MobObj obj; // ���� ���� ������ ��ũ���ͺ� ������Ʈ

    [HideInInspector]
    public Rigidbody2D RB; // ���� ������ �ٵ� 

    [HideInInspector]
    public GameObject Mob; // ���� ������Ʈ

    [HideInInspector]
    public GameObject Target; // Ÿ�� �÷��̾� ������Ʈ

    [HideInInspector]
    public bool isUsingSkillState = false; // ��ų ��� ���� ����

    [HideInInspector]
    public float dis = 0; // �÷��̾���� �Ÿ�

    [HideInInspector]
    public Vector2 dir = Vector2.zero; // �÷��̾��� ���� 


    IMobSkill[] mobSkills;

    private void Start()
    {

        Mob = gameObject;

        Target = GameObject.FindWithTag("Player"); // ���� �� ��� ����

        RB = GetComponent<Rigidbody2D>();

        mobSkills = GetComponents<IMobSkill>(); // ��ų ��ũ��Ʈ ������Ʈ

        foreach (IMobSkill skill in mobSkills)
        {

            skill.Init();

        }

    }

    private void Update()
    {

        dis = Vector2.Distance(transform.position, Target.transform.position); // �Ÿ� �� ���
        dir = (Target.transform.position - transform.position).normalized; // ���� ���

        Routine_Move(dis, dir);
        Routine_Attack(dis, dir);

    }

    private void Routine_Move(float dis, Vector2 dir) // �̵� ��ƾ
    {

        if(!isUsingSkillState) // ��ų ��� ���°� �ƴ� ��쿡��
        {

            if (obj.MoveType == AI_MoveType.Normal) // �ܼ� ����
            {

                RB.MovePosition(RB.position + dir * obj.speed * Time.deltaTime); // �÷��̾� �������� �̵�

            }
            else if (obj.MoveType == AI_MoveType.Distance) // �Ÿ� ����
            {

                if (obj.Distance_Range < dis) // �Ÿ� ���� ���̶�� ����
                {

                    RB.MovePosition(RB.position + dir * obj.speed * Time.deltaTime); // �÷��̾� �������� �̵�

                }

            }

        }

    }

    private void Routine_Attack(float dis, Vector2 dir) // ���� ��ƾ
    {

        if (obj.AttackType == AI_AttackType.Simple)
        {



        }
        else if (obj.AttackType == AI_AttackType.Skill)
        {

            if (obj.Attack_Range >= dis) // ���� ���̶�� ��ų ���
            {

                Skill_Use();

            }

        }

    }

    private void Skill_Use() // ��ų ���
    {

        foreach(IMobSkill skill in mobSkills)
        {

            if (!skill.coolDown && skill.data && skill.data.skillTag != MobSkillTag.Dead)
            {

                skill.coolDown = true;
                isUsingSkillState = true;

                StartCoroutine(Skill_Cooltime(skill));

                skill.Use(this);

            }

        }

    }

    private IEnumerator Skill_Cooltime(IMobSkill skill) // ��ų ��Ÿ��
    {
        yield return new WaitForSeconds(skill.data.coolTime); // ���

        skill.coolDown = false;
        isUsingSkillState = false;

    }

}

