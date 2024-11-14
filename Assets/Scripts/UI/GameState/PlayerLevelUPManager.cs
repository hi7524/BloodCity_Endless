using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUPManager : MonoBehaviour
{
    public static PlayerLevelUPManager Instance;

    private string[] singleStats = { "surviveStats", "strengthStats", "intellectStats" };
    private string[] multiStats = { "SurStr", "StrInt", "IntSur" };

    public Image[] upgradeCards; // UpgradeState
    public Button card3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GenerateLevelUpCards()
    {
        // 카드 1, 2 생성
        GenerateSingleStatCards();

        // 카드 3 생성
        if (Random.Range(0f, 1f) < 0.3f) // 30% 확률
        {
            card3.interactable = true;
            GenerateMultiStatCard();
        }
        else
        {
            upgradeCards[2].sprite = Resources.Load<Sprite>("States/null");
            card3.interactable = false;
        }
    }

    void GenerateSingleStatCards()
    {
        string randomIndex1 = singleStats[Random.Range(0, singleStats.Length)];
        string randomIndex2;

        // 두 번째 인덱스가 첫 번째 인덱스와 같지 않도록
        do { randomIndex2 = singleStats[Random.Range(0, singleStats.Length)]; } while (randomIndex2 == randomIndex1);

        upgradeCards[0].sprite = Resources.Load<Sprite>("States/" + randomIndex1);
        upgradeCards[1].sprite = Resources.Load<Sprite>("States/" + randomIndex2);

        Debug.Log($"Selected Stat 1: {randomIndex1}");
        Debug.Log($"Selected Stat 2: {randomIndex2}");
    }

    void GenerateMultiStatCard()
    {
        string randomStat = multiStats[Random.Range(0, multiStats.Length)];

        upgradeCards[2].sprite = Resources.Load<Sprite>("States/" + randomStat);

        Debug.Log($"Selected Stat 3: {randomStat}");
    }
}
