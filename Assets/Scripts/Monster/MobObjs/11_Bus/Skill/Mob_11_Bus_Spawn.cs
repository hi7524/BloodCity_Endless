using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_11_Bus_Spawn : MonoBehaviour, IMobSkill
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

        StartCoroutine(Delay(AI));

    }

    private IEnumerator Delay(MobAI AI)
    {

        AI.isUsingSkillState = true;
        AI.GetComponent<Animator>().SetBool("isSummon", true);
        AI.Mob.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < 10; i++)
        {
            MobAI ai = Instantiate(mobs[Random.Range(0, mobs.Length)], AI.gameObject.transform.position + new Vector3(Random.Range(-5f, 6f), -2.75f), AI.gameObject.transform.rotation)
                .GetComponent<MobAI>();

            ai.isInstantSpawn = true;

            TimeManager spawner = TimeManager.Instance;

            ai.Init(spawner != null ? spawner.hpPers[spawner.nowMin] : 1);

            yield return new WaitForSeconds(0.1f);
        }

        AI.GetComponent<Animator>().SetBool("isSummon", false);
        AI.isUsingSkillState = false;

    }

}