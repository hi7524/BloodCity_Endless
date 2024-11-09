using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaStateReset : MonoBehaviour
{
    public GameObject ChaReset;

    void Update()
    {
        if (ChaReset.activeSelf == true)
        {
            //ChaSelect.Instance.currentIndex = 0;
            ChaSelect.Instance.PermanentStat();
            ChaSelect.Instance.UpdateCharacterInfo();
        }
    }
}
