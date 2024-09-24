using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public TMP_Text coinsText; // 현재 코인 텍스트
    public TMP_Text stateDescriptionText; // 스탯 설명 텍스트
    public TMP_Text upgradeCostText; // 업그레이드 비용 텍스트
    public Button[] stateButtons; // 스탯 버튼 배열

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
