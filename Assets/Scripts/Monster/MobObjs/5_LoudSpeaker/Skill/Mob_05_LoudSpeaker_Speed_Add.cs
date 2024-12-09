using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_05_LoudSpeaker_Speed_Add : MonoBehaviour
{

    private void Start()
    {
        GetComponent<MobAI>().speed *= 1.2f;
    }

}
