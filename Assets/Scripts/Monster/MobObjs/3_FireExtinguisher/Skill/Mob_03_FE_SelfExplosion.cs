using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_03_FE_SelfExplosion : MonoBehaviour, IMobSkill
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

        AI.GetComponent<SpriteRenderer>().sortingOrder = -4;

        GameObject Fire = Instantiate(data.SkillEffect, AI.gameObject.transform.position, AI.gameObject.transform.rotation);

        StartCoroutine(Remove(Fire, AI));

    }

    private IEnumerator Remove(GameObject obj, MobAI AI)
    {

        yield return new WaitForSeconds(1.5f);

        Destroy(obj);
        AI.Dead();

    }

}