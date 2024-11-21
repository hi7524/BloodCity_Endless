using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_11_Bus_Rush : MonoBehaviour, IMobSkill
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

        StartCoroutine(Delay(AI));

    }

    private IEnumerator Delay(MobAI AI)
    {

        AI.isUsingSkillState = true;
        AI.isSkillBanState = true;

        AI.GetComponent<Animator>().SetInteger("Rush_Act", 1);
        AI.Mob.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        yield return new WaitForSeconds(1.3f);

        AI.AddSpeed(5000);
        AI.bodyAttackDamage = 40;

        AI.GetComponent<Animator>().SetInteger("Rush_Act", 2);
        AI.isUsingSkillState = false;

        yield return new WaitForSeconds(3f);

        AI.isUsingSkillState = true;

        AI.GetComponent<Animator>().SetInteger("Rush_Act", 3);
        AI.Mob.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        AI.ResetSpeed();
        AI.bodyAttackDamage = 30;   
            
        yield return new WaitForSeconds(1.2f);

        AI.GetComponent<Animator>().SetInteger("Rush_Act", 0);

        AI.isUsingSkillState = false;
        AI.isSkillBanState = false;

    }

}