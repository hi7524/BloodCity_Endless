using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Mob_09_Beast_Rush : MonoBehaviour, IMobSkill
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

        AI.GetComponent<Animator>().SetBool("isUseSkill", true);
        StartCoroutine(MoveDelay(AI));

    }


    private IEnumerator MoveDelay(MobAI AI)
    {
        for(int i = 1; i <= 3; i++)
        {

            AI.isUsingSkillState = true;

            var dis = Vector2.Distance(transform.position, AI.Target.transform.position); // 거리 차 계산
            var dir = (AI.Target.transform.position - transform.position).normalized; // 방향 계산
            AI.render.flipX = (!AI.isReverseSprite ? dir.x > 0 : dir.x < 0); // 방향 전환

            AI.Mob.GetComponent<Rigidbody2D>().AddForce(dir * 12f, ForceMode2D.Impulse);

            yield return new WaitForSeconds(1f);
            AI.Mob.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        }

        AI.isUsingSkillState = false;
        AI.GetComponent<Animator>().SetBool("isUseSkill", false);

    }

}