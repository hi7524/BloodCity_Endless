using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_08_Camera_Flash_Coll : MonoBehaviour
{

    PlayerState playerState;
    float speed;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        
        if(coll.tag == "Player")
        {

            playerState = coll.GetComponent<PlayerState>();

            speed = playerState.speed;
            playerState.speed = 0;

            StartCoroutine(Remove());

        }

    }

    void RollBackSpeed()
    {

        if(playerState != null)
            playerState.speed = speed;

    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(0.5f);
        RollBackSpeed();
    }

    private void OnDestroy()
    {

        RollBackSpeed();

    }

}