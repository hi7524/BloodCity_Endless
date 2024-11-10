using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_06_Mushroom_Spawn : MonoBehaviour, IMobSkill
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

        GameObject obj = Instantiate(data.SkillEffect, AI.gameObject.transform.position, AI.gameObject.transform.rotation);

        obj.GetComponent<MobAI>().isInstantSpawn = true;

    }

}