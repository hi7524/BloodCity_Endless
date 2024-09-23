using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSkillDamage : MonoBehaviour
{

    public int init_damage = 0;
    public bool isDelete = false; // 충돌 시 제거 여부

    private void OnTriggerEnter2D(Collider2D coll)
    {

        GameObject Player = coll.gameObject;

        if(Player.tag == "Player")
        {

            Player.GetComponent<PlayerHealth>().Damaged(init_damage); // 피해 적용

            if (isDelete)
                Destroy(gameObject);

        }

    }

}
