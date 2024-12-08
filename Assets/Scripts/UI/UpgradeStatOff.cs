using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStatOff : MonoBehaviour
{
    public void WaitForAnimationEnd()
    {
        UIManager.Instance.levelUpWindow.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
