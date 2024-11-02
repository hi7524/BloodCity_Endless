using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 설명
public class TestMonster : MonoBehaviour
{
    public float health = 100;

    public void DamageTest(float damage)
    {
        health -= damage; 
        Debug.Log(gameObject.name +": " +health);
    }
}
