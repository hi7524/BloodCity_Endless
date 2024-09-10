using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpSystem : MonoBehaviour
{
    [SerializeField] PlayerXpData playerXP;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerEXP>().AddPlayerXP(playerXP.point);
            Destroy(gameObject);
        }
    }
}
