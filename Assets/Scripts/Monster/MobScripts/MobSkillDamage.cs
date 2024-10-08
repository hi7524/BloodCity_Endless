using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MobSkillDamageType { Single, Multiple_Sec }

public class MobSkillDamage : MonoBehaviour
{

    public MobSkillDamageType damageType;
    public int init_damage = 0;
    public bool isDelete = false; // 단일 공격 : 충돌 시 제거 여부

    private bool isCanAttack = true; // 연속 공격 : 공격 가능 여부

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if(damageType == MobSkillDamageType.Single) // 단일 공격 유형
        {

            GameObject Player = coll.gameObject;

            if (Player.tag == "Player")
            {

                Player.GetComponent<PlayerHealth>().Damaged(init_damage); // 피해 적용

                if (isDelete)
                    Destroy(gameObject);

            }

        }

    }

    private void OnTriggerStay2D(Collider2D coll)
    {

        if(isCanAttack)
        {

            if (damageType == MobSkillDamageType.Multiple_Sec) // 연속 공격 유형 - 초당 피해
            {

                GameObject Player = coll.gameObject;

                if (Player.tag == "Player")
                {

                    isCanAttack = false;
                    Player.GetComponent<PlayerHealth>().Damaged(init_damage); // 피해 적용

                    Invoke("AttackDelay", 1);

                }

            }

        }

    }

    private void AttackDelay() // 공격 딜레이 타이머
    {

        isCanAttack = true;

    }

}