using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStatOff : MonoBehaviour
{
    public void WaitForAnimationEnd()
    {
        Debug.Log("왜안껴ㅓ져");
        UIManager.Instance.levelUpWindow.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
