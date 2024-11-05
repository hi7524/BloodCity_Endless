using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_03_FE_Fire : MonoBehaviour, IMobSkill
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

        Fire.GetComponent<Rigidbody2D>().AddForce(AI.dir * 3f, ForceMode2D.Impulse);
        Fire.GetComponent<MobSkillDamage>().init_damage = AI.obj.attackDamage;

        Fire.GetComponent<Mob_03_FE_Fire_Destroy>().StartRemove();

    }

}