using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Stat
{
    public string name; // 스탯 이름
    public float value; // 현재 값
    public string description; // 설명

    // 각 단계별 업그레이드 정보
    public UpgradeInfo[] upgradeInfo; // 업그레이드 정보 배열

    public Stat(string name, float initialValue, string description, UpgradeInfo[] upgradeInfo)
    {
        this.name = name;
        this.value = initialValue;
        this.description = description;
        this.upgradeInfo = upgradeInfo;
    }

    // 업그레이드 정보를 담는 클래스
    [System.Serializable]
    public class UpgradeInfo
    {
        public float increaseAmount; // 증가량
        public int cost; // 업그레이드 비용

        public UpgradeInfo(float increaseAmount, int cost)
        {
            this.increaseAmount = increaseAmount;
            this.cost = cost;
        }
    }
}

public class PlayerStats
{
    public Stat[] stats; // 9개의 스탯 배열
    public int coins; // 현재 코인 수

    public PlayerStats()
    {
        stats = new Stat[]
        {
            new Stat("Max Health", 100, "",new Stat.UpgradeInfo[]
            {
                new Stat.UpgradeInfo(10, 50),  // 1단계
                new Stat.UpgradeInfo(10, 100), // 2단계
                new Stat.UpgradeInfo(10, 150)  // 3단계
            }),
            new Stat("Restore Per Sec", 0, "",new Stat.UpgradeInfo[]
            {
                new Stat.UpgradeInfo(0.15f, 30),
                new Stat.UpgradeInfo(0.15f, 60),
                new Stat.UpgradeInfo(0.20f, 90)
            }),
            // 나머지 스탯도 같은 방식으로 초기화
        };
        coins = 100; // 초기 코인 수
    }
}

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public PlayerStats playerStats; // 플레이어 스탯
    public TMP_Text coinsText; // 현재 코인 텍스트
    public TMP_Text statDescriptionText; // 스탯 설명 텍스트
    public TMP_Text upgradeCostText; // 업그레이드 비용 텍스트
    public Button[] statButtons; // 스탯 버튼 배열

    private int selectedStatIndex = -1; // 선택된 스탯 인덱스
    private int currentUpgradeLevel = 0; // 현재 업그레이드 단계

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        // 데이터 로드
        LoadPlayerData();
        UpdateUI();
    }

    private void LoadPlayerData()
    {
        // 플레이어 데이터 로드
        playerStats = DataManager.Instance.player.playerStats;
    }

    private void UpdateUI()
    {
        // 코인 수 업데이트
        coinsText.text = $"현재 코인: {playerStats.coins}";

        // 버튼 초기화
        for (int i = 0; i < statButtons.Length; i++)
        {
            int index = i; // 로컬 변수 사용
            statButtons[i].onClick.AddListener(() => OnStatButtonClick(index));
        }
    }

    private void OnStatButtonClick(int index)
    {
        // 선택된 스탯 인덱스 업데이트
        selectedStatIndex = index;

        // 스탯 설명 및 업그레이드 비용 업데이트
        UpdateStatInfo();
    }

    private void UpdateStatInfo()
    {
        if (selectedStatIndex >= 0)
        {
            Stat selectedStat = playerStats.stats[selectedStatIndex];
            var upgradeInfo = selectedStat.upgradeInfo[currentUpgradeLevel];
            statDescriptionText.text = selectedStat.description;
            upgradeCostText.text = $"업그레이드 비용: {upgradeInfo.cost}";
            
            // 마우스 오버 시 기능 추가 가능
        }
    }

    public void UpgradeStat()
    {
        if (selectedStatIndex < 0 || currentUpgradeLevel >= playerStats.stats[selectedStatIndex].upgradeInfo.Length) return;

        Stat selectedStat = playerStats.stats[selectedStatIndex];
        var upgradeInfo = selectedStat.upgradeInfo[currentUpgradeLevel];

        // 코인 체크 및 스탯 업그레이드
        if (playerStats.coins >= upgradeInfo.cost)
        {
            playerStats.coins -= upgradeInfo.cost;
            selectedStat.value += upgradeInfo.increaseAmount;

            // 업그레이드 후 데이터 저장
            DataManager.Instance.player.playerStats = playerStats;
            DataManager.Instance.Save();

            UpdateUI();
        }
    }

    public void NextUpgradeLevel()
    {
        // 다음 단계로 넘어가는 로직
        if (currentUpgradeLevel < playerStats.stats[selectedStatIndex].upgradeInfo.Length - 1)
        {
            currentUpgradeLevel++;
            UpdateStatInfo();
        }
    }
}
