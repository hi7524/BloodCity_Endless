using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public DataManager DataManager { get; private set; }
    public CharacterStates playerStates; // 여기에 Player 스탯

    public Text coinsText; // 현재 코인 텍스트
    public Button[] stateButtons; // 스탯 버튼 배열
    public int engauge; // 강화 횟수
    public Image[] gauge; // 게이지 배열
    public EnhanceState[] enhanceStates; // 인챈트 스탯 배열
    public int currentCoins; // 현재 가진 돈, 초기값 설정
    public int selectedIndex = 0; // 선택된 스탯 인덱스

    public GameObject statTap;
    public GameObject skillTap;
    public GameObject select;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "PlayerData.json"))
        {
            LoadStates();
        }

        UpdateCoinsText();
        GaugeUp();
    }

    public void ResetStates()
    {
        playerStates.Initialize(); // CharacterStates 초기화
        foreach (var enhanceState in enhanceStates)
        {
            enhanceState.Initialize(); // EnhanceState 초기화
        }

        for (int i = 0; i < stateButtons.Length; i++)
        {
            stateButtons[i].interactable = true;
        }
        currentCoins = DataManager.Instance.player.coins;

        Debug.Log("<color=lime>[SUCCESS]</color> 스탯 데이터 초기화 완료");
    }

    public void LoadStates()
    {
        currentCoins = DataManager.Instance.player.coins;
        engauge = DataManager.Instance.player.engauge;
        playerStates.maxHealth = DataManager.Instance.player.characterStats[0];
        playerStates.restorePerSec = DataManager.Instance.player.characterStats[1];
        playerStates.defense = DataManager.Instance.player.characterStats[2];
        playerStates.speed = DataManager.Instance.player.characterStats[3];
        playerStates.attackDamage = DataManager.Instance.player.characterStats[4];
        playerStates.attackRange = DataManager.Instance.player.characterStats[5];
        playerStates.abilityHaste = DataManager.Instance.player.characterStats[6];
        playerStates.magnetism = DataManager.Instance.player.characterStats[7];
        playerStates.curse = DataManager.Instance.player.characterStats[8];

        for (int i = 0; i < enhanceStates.Length; i++)
        {
            enhanceStates[i].enhanceLevel = DataManager.Instance.player.enhanceLevels[i];
            enhanceStates[i].isUpgraded = DataManager.Instance.player.isUpgraded[i];

            if (enhanceStates[i].isUpgraded == true)
            {
                stateButtons[i].interactable = false;
            }
        }

        Debug.Log("<color=lime>[SUCCESS]</color> 스탯 데이터 불러오기 완료");
    }

    public void PickState(int index)
    {
        selectedIndex = index; // 선택된 스탯 인덱스를 저장
        select.SetActive(true);
    }

    public void Upgrade()
    {
        // 업그레이드 정보
        float cost = enhanceStates[selectedIndex].stateUpgrade[0].cost; // 첫 번째 업그레이드 비용
        string statName = enhanceStates[selectedIndex].stateName; // 스탯 이름

        // 업그레이드 가능 여부 체크
        if (currentCoins >= cost)
        {
            // 스탯 업그레이드
            float increaseAmount = enhanceStates[selectedIndex].stateUpgrade[0].increaseAmount;

            if (enhanceStates[selectedIndex].stateUpgrade[0].isPercentage)
            {
                float currentStat = playerStates.GetStat(statName);
                playerStates.SetStat(statName, currentStat * (1 + increaseAmount / 100));
            }
            else
            {
                float currentStat = playerStates.GetStat(statName);
                playerStates.SetStat(statName, currentStat + increaseAmount);
            }

            currentCoins -= (int)cost; // 비용 차감
            enhanceStates[selectedIndex].enhanceLevel += 1; // 단계를 올리고
            enhanceStates[selectedIndex].isUpgraded = true; // 업그레이드 했다고 표시하고
            stateButtons[selectedIndex].interactable = false; // 상호작용 끄기
            engauge += 1;
            GaugeUp();
            select.SetActive(false);

            DataManager.Instance.player.engauge = engauge;
            DataManager.Instance.player.coins = currentCoins;

            DataManager.Instance.player.characterStats[0] = playerStates.maxHealth;
            DataManager.Instance.player.characterStats[1] = playerStates.restorePerSec;
            DataManager.Instance.player.characterStats[2] = playerStates.defense;
            DataManager.Instance.player.characterStats[3] = playerStates.speed;
            DataManager.Instance.player.characterStats[4] = playerStates.attackDamage;
            DataManager.Instance.player.characterStats[5] = playerStates.attackRange;
            DataManager.Instance.player.characterStats[6] = playerStates.abilityHaste;
            DataManager.Instance.player.characterStats[7] = playerStates.magnetism;
            DataManager.Instance.player.characterStats[8] = playerStates.curse;

            for (int i = 0; i < enhanceStates.Length; i++)
            {
                DataManager.Instance.player.enhanceLevels[i] = enhanceStates[i].enhanceLevel;
                DataManager.Instance.player.isUpgraded[i] = enhanceStates[i].isUpgraded;
            }
            DataManager.Instance.Save();

            // 코인 텍스트 업데이트
            UpdateCoinsText();
        }
        else
        {
            Debug.Log("<color=orange>[WARNING]</color> 코인이 부족합니다!");
        }
    }

    public void UpdateCoinsText()
    {
        coinsText.text = $"Coin : {currentCoins}";
    }

    public void GaugeUp()
    {
        if (engauge == 0)
        {
            for (int i = 0; i < enhanceStates.Length; i++) { gauge[i].GetComponent<Image>().color = Color.white; }
        }
        else if (engauge > 0)
        {
            for (int i = 0; i < engauge; i++) { gauge[i].GetComponent<Image>().color = Color.green; }
        }
    }

    // 단계 하나 다 깨면 스킬 열리는 거랑 다시 업글 가능하게 푸는 건 추후 제작

    public void TapSwitch(int tap)
    {
        if (tap == 0)
        {
            statTap.SetActive(true);
            skillTap.SetActive(false);
        }
        else if (tap == 1)
        {
            statTap.SetActive(false);
            skillTap.SetActive(true);
        }
    }
}