using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 샷건
public class TESTShotGun : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<PlayerSkillPos>().AddSkill();
        }

    }
}
