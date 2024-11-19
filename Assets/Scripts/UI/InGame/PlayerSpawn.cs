using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public static PlayerSpawn Instance;

    public GameObject spaceMarine;
    public GameObject beeper;
    public GameObject baz;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Spawn()
    {
        /*if (DataManager.Instance.player.currentIndex == 0) { spaceMarine.SetActive(true); }
        if (DataManager.Instance.player.currentIndex == 1) { beeper.SetActive(true); }
        if (DataManager.Instance.player.currentIndex == 2) { baz.SetActive(true); }*/
    }
}
