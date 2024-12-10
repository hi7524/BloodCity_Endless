using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunOn : MonoBehaviour
{
    public GameObject[] gun = new GameObject[5];
    
    void Start()
    {
        if (DataManager.Instance.player.gun) { foreach (var gun in gun) { gun.SetActive(true); } }
        else { foreach (var gun in gun) { gun.SetActive(false); } }
    }
}
