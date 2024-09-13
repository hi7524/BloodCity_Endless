using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MobSkillTag { Attack, Skill, Dead } // 몬스터 스킬 태그

public interface IMobSkill
{

    public MobSkillData data { get; set; } // 스킬 데이터

    public bool coolDown { get; set; } // 현재 쿨타임

    public void Init() // 초기화
    {
        data = data ?? new MobSkillData();
    }

    public void Use(MobAI AI); // 스킬 사용

}