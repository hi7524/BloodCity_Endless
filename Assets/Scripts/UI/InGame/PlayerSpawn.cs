using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public static PlayerSpawn Instance;

    public GameObject cha;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Spawn()
    {
        
    }
}
