using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_02_Bird_Rush : MonoBehaviour, IMobSkill
{

    public MobSkillData skillData; // ��ų ������
    public MobSkillData data { get; set; } // ��ų ������

    public bool coolDown { get; set; } // ���� ��Ÿ��

    public void Init()
    {

        data = skillData;

    }

    public void Use(MobAI AI)
    {

        AI.Mob.GetComponent<Rigidbody2D>().AddForce(AI.dir * 10, ForceMode2D.Impulse);

    }

}
