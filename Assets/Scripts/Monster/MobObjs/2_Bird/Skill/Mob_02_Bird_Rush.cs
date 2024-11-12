using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_02_Bird_Rush : MonoBehaviour, IMobSkill
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

        AI.isUsingSkillState = true;

        AI.Mob.GetComponent<Rigidbody2D>().AddForce(AI.dir * 17.5f, ForceMode2D.Impulse);

        StartCoroutine(MoveDelay(AI));

    }

    private IEnumerator MoveDelay(MobAI AI)
    {

        yield return new WaitForSeconds(1.25f);

        AI.isUsingSkillState = false;

        AI.Mob.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

    }

}