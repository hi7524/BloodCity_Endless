using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_08_Camera_Flash_Sturn : MonoBehaviour
{
    bool isSturn = true;
    public float sturnSec;
    float originSpeed;

    public void AddSturn(float sec)
    {

        sturnSec += sec;

    }

    public void SetSturn(float sec)
    {
        PlayerState state = PlayerState.Instance;

        originSpeed = state.speed;
        state.speed = 0;

        sturnSec = sec;

        isSturn = true;
    }
    public void RemoveSturn()
    {

        PlayerState.Instance.speed += originSpeed;
        Destroy(GetComponent<Mob_08_Camera_Flash_Sturn>());

    }

    void Update()
    {

        if (isSturn)
        {
            sturnSec -= Time.deltaTime;

            if (sturnSec <= 0)
                RemoveSturn();
        }

    }

}