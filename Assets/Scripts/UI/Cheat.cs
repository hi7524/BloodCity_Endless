using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    public Toggle Gun;
    public Toggle Min;

    public void Start()
    {
        if (DataManager.Instance.player.gun) { Gun.isOn = true; }
        else { Gun.isOn = false; }
        if (DataManager.Instance.player.min) { Min.isOn = true; }
        else { Min.isOn = false; }
    }

    public void GunOn()
    {
        if (Gun.isOn) { DataManager.Instance.player.gun = true; }
        else { DataManager.Instance.player.gun = false; }
        DataManager.Instance.Save();
    }

    public void MinOn()
    {
        if (Min.isOn) { DataManager.Instance.player.min = true; }
        else { DataManager.Instance.player.min = false; }
        DataManager.Instance.Save();
    }
}