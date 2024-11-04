using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    public int stageIndex;

    public void PickStage(int pickstage)
    {
        stageIndex = pickstage;
    }

    public void PickSelect()
    {
        DataManager.Instance.player.stageIndex = stageIndex;
        DataManager.Instance.Save();
    }
}
