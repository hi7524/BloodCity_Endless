using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    public Image Stage;
    public Sprite[] StageImg;
    public int stageIndex;

    public GameObject pick;

    public void ShowNextStage(string LR)
    {
        // 현재 인덱스를 업데이트
        if (LR == "L")
        {
            stageIndex = (stageIndex - 1 + StageImg.Length) % StageImg.Length;
            Stage.sprite = StageImg[stageIndex];
        }
        else if (LR == "R")
        {
            stageIndex = (stageIndex + 1) % StageImg.Length;
            Stage.sprite = StageImg[stageIndex];
        }

        if (stageIndex == 0) { pick.SetActive(true); }
        else { pick.SetActive(false); }
    }

    public void PickSelect()
    {
        DataManager.Instance.player.stageIndex = stageIndex;
        DataManager.Instance.Save();
    }
}
