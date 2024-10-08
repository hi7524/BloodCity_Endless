using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_10_Tree_Init : MonoBehaviour, IMobSkill
{

    public MobSkillData skillData; // 스킬 데이터
    public MobSkillData data { get; set; } // 스킬 데이터

    public bool coolDown { get; set; } // 현재 쿨타임

    public void Init()
    {

        data = skillData;

    }

    public void Use(MobAI AI)
    {

        AI.hp = AI.hp * GameManager.Instance.playerLevel; // 플레이어 레벨 비례 체력 설정

    }

}