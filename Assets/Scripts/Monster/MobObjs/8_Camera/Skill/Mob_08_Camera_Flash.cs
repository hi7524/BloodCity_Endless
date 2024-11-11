using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_08_Camera_Flash : MonoBehaviour, IMobSkill
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

        GameObject obj = Instantiate(data.SkillEffect, AI.gameObject.transform.position, AI.gameObject.transform.rotation, AI.transform);

        obj.transform.localPosition = new Vector3(-1, 0.45f, 0);

       
    }

    private void Update()
    {
        
        

    }

    private IEnumerator Remove()
    {

        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);

    }

}
