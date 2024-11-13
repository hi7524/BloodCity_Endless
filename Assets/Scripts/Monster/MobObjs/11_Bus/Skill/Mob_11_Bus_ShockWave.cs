using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_11_Bus_ShockWave : MonoBehaviour, IMobSkill
{

    public MobSkillData skillData; // 스킬 데이터
    public MobSkillData data { get; set; } // 스킬 데이터

    public bool coolDown { get; set; } // 현재 쿨타임

    GameObject obj;

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
        AI.Mob.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        AI.GetComponent<Animator>().SetInteger("ShockWave_Act", 1);

        yield return new WaitForSeconds(1f);

        AI.GetComponent<Animator>().SetInteger("ShockWave_Act", 2);

        yield return new WaitForSeconds(0.4f);

        obj = Instantiate(data.SkillEffect, AI.gameObject.transform.position + new Vector3(0, -3f), Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        Destroy(obj);

        yield return new WaitForSeconds(0.4f);

        AI.GetComponent<Animator>().SetInteger("ShockWave_Act", 0);
        AI.isUsingSkillState = false;

    }

    private void OnDestroy()
    {
        if (obj != null)
            Destroy(obj);
    }

}