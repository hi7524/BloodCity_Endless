using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mob_08_Camera_Flash_Coll : MonoBehaviour
{

    PlayerState playerState;
    float speed;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        
        if(coll.tag == "Player")
        {

            Mob_08_Camera_Flash_Sturn Sturn = coll.GetComponent<Mob_08_Camera_Flash_Sturn>();

            if (Sturn == null)
            {
                coll.AddComponent<Mob_08_Camera_Flash_Sturn>().SetSturn(0.5f);
            }
            else
                Sturn.AddSturn(0.5f);

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