using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUPManager : MonoBehaviour
{
    public static PlayerLevelUPManager Instance;

    private string[] surviveStats = { "maxHealth", "restorePerSec", "defense", "speed" };
    private string[] strengthStats = { "attackDamage", "attackRange" };
    private string[] intellectStats = { "abilityHaste", "magnetism" };

    public Image[] upgradeCards; // UpgradeState 게임 오브젝트의 이미지 배열
    public Sprite defaultSprite; // 기본 상태의 스프라이트
    public Sprite selectedSprite; // 선택 상태의 스프라이트

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

        // 카드 3 생성 (30% 확률)
        if (Random.Range(0f, 1f) < 0.3f) // 30% 확률
        {
            GenerateMultiStatCard();
        }
    }

    void GenerateSingleStatCards()
    {
        List<string[]> statCategories = new List<string[]> { surviveStats, strengthStats, intellectStats };
        int[] selectedIndices = new int[2];
        selectedIndices[0] = Random.Range(0, statCategories.Count);
        do
        {
            selectedIndices[1] = Random.Range(0, statCategories.Count);
        } while (selectedIndices[1] == selectedIndices[0]);

        string cardOneStat = statCategories[selectedIndices[0]][Random.Range(0, statCategories[selectedIndices[0]].Length)];
        string cardTwoStat = statCategories[selectedIndices[1]][Random.Range(0, statCategories[selectedIndices[1]].Length)];

        Debug.Log($"Card 1 Stat: {cardOneStat}");
        Debug.Log($"Card 2 Stat: {cardTwoStat}");

        ChangeCardImage(0, cardOneStat);
        ChangeCardImage(1, cardTwoStat);
    }

    void GenerateMultiStatCard()
    {
        List<string[]> allStats = new List<string[]> { surviveStats, strengthStats, intellectStats };
        List<string> selectedStats = new List<string>();

        for (int i = 0; i < 2; i++)
        {
            string[] randomCategory = allStats[Random.Range(0, allStats.Count)];
            selectedStats.Add(randomCategory[Random.Range(0, randomCategory.Length)]);
        }

        Debug.Log($"Card 3 Stat 1: {selectedStats[0]}");
        Debug.Log($"Card 3 Stat 2: {selectedStats[1]}");

        // 카드 이미지 변경 (두 개의 스탯을 전달)
        ChangeCardImage(2, selectedStats[0], selectedStats[1]);
    }

    void ChangeCardImage(int cardIndex, string stat1, string stat2 = null)
    {
        Sprite normalSprite;
        Sprite selectedSprite;

        if (stat2 != null)
        {
            // 기본 상태 스프라이트 이름 조합
            string normalSpriteName = $"{stat1}_{stat2}"; // 예: "maxHealth_restorePerSec"
            normalSprite = Resources.Load<Sprite>("States/" + normalSpriteName);

            // 선택된 상태 스프라이트 이름 조합
            string selectedSpriteName = $"{stat1}_{stat2}_1"; // 예: "maxHealth_restorePerSec_1"
            selectedSprite = Resources.Load<Sprite>("States/" + selectedSpriteName);
        }
        else
        {
            // 단일 스탯일 경우
            normalSprite = Resources.Load<Sprite>("States/" + stat1); // 예: "maxHealth"
            selectedSprite = Resources.Load<Sprite>("States/" + stat1 + "_1"); // 예: "maxHealth_1"
        }

        // 카드의 기본 이미지 설정
        if (normalSprite != null)
        {
            upgradeCards[cardIndex].sprite = normalSprite;
            UpdateButtonSprites(upgradeCards[cardIndex].GetComponent<Button>(), normalSprite, selectedSprite);
        }
        else
        {
            Debug.LogWarning("Normal sprite not found for: " + (stat2 != null ? $"{stat1}_{stat2}" : stat1));
        }
    }

    void UpdateButtonSprites(Button button, Sprite normalSprite, Sprite selectedSprite)
    {
        SpriteState spriteState = button.spriteState;

        // Normal 상태 스프라이트 할당
        spriteState.disabledSprite = normalSprite;

        // Highlighted 상태 스프라이트 할당 (선택된 이미지로 설정)
        if (selectedSprite != null)
        {
            spriteState.highlightedSprite = selectedSprite;
            spriteState.pressedSprite = selectedSprite;
            spriteState.selectedSprite = selectedSprite;
        }

        button.spriteState = spriteState; // 버튼에 새로운 스프라이트 상태 적용
    }  
}
