using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawn : MonoBehaviour
{
    public static PlayerSpawn Instance;

    public string character;

    public GameObject SpaceMarine;
    public GameObject Beeper;
    public GameObject Baz;

    public CinemachineVirtualCamera virtualCam;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        switch (DataManager.Instance.player.currentIndex)
        {
            case 0:
                Destroy(Beeper);
                Destroy(Baz);
                virtualCam.Follow = SpaceMarine.transform;
                break;
            case 1:
                Destroy(SpaceMarine);
                Destroy(Baz);
                virtualCam.Follow = Beeper.transform;
                break;
            case 2:
                Destroy(SpaceMarine);
                Destroy(Beeper);
                virtualCam.Follow = Baz.transform;
                break;
        }
    }
}
