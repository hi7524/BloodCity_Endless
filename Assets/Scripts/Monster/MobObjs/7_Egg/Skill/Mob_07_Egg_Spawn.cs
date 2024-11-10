using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_07_Egg_Spawn : MonoBehaviour, IMobSkill
{

    public MobSkillData skillData; // 스킬 데이터
    public MobSkillData data { get; set; } // 스킬 데이터

    public bool coolDown { get; set; } // 현재 쿨타임

    public GameObject[] mobs;

    public void Init()
    {
        data = skillData;
    }

    public void Use(MobAI AI)
    {

        Instantiate(data.SkillEffect, AI.gameObject.transform.position, AI.gameObject.transform.rotation);

        for(int i = 0; i < 10; i++)
            Instantiate(mobs[Random.Range(0, mobs.Length)], AI.gameObject.transform.position + new Vector3(Random.Range(-5, 6), Random.Range(-5, 6)), AI.gameObject.transform.rotation)
                .GetComponent<MobAI>().isInstantSpawn = true;
        
        AI.Dead();

    }

}