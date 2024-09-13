using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MobSkillTag { Attack, Skill, Dead } // ���� ��ų �±�

public interface IMobSkill
{

    public MobSkillData data { get; set; } // ��ų ������

    public bool coolDown { get; set; } // ���� ��Ÿ��

    public void Init() // �ʱ�ȭ
    {
        data = data ?? new MobSkillData();
    }

    public void Use(MobAI AI); // ��ų ���

}