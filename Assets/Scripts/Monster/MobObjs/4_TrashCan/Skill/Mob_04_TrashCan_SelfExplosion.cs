using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_04_TrashCan_SelfExplosion : MonoBehaviour, IMobSkill
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

        GameObject Fire = Instantiate(data.SkillEffect, AI.gameObject.transform.position, AI.gameObject.transform.rotation);

        StartCoroutine(Remove(Fire, AI));

        AI.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);

    }

    private IEnumerator Remove(GameObject obj, MobAI AI)
    {

        AI.GetComponent<Animator>().Play("Dead");

        yield return new WaitForSeconds(3f);

        Destroy(obj);
        AI.Dead();
        
    }

}