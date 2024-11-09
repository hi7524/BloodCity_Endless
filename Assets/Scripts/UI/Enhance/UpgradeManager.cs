using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public DataManager DataManager { get; private set; }

    [Header("Data")]
    public CharacterStates playerStates; // 여기에 Player 스탯
    public EnhanceState[] enhanceStates; // 인챈트 스탯 배열

    [Header("Coin")]
    public Text coinsText; // 현재 코인 텍스트
    public int currentCoins; // 현재 가진 돈, 초기값 설정

    [Header("StatSelect")]
    public Button[] stateButtons; // 스탯 버튼 배열
    public int selectedIndex = 0; // 선택된 스탯 인덱스

    [Header("LevelGauge")]
    public TMP_Text levelText; // 단계 텍스트
    public int level = 1; // 현재 단계
    public int engauge; // 강화 횟수
    public Image[] gauge; // 게이지 배열

    [Header("Skill")]
    public SkillStates[] skillStates = new SkillStates[3]; // 스킬 스탯 배열
    public Button[] Skills = new Button[3]; // 스킬 버튼
    public int selectedSkillIndex = 0; // 선택된 스킬 인덱스

    [Header("Tap")]
    public GameObject statTap;
    public GameObject skillTap;
    public GameObject select;
    public GameObject selectskill;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        LoadStates();
        UpdateCoinsText();
        GaugeUp();
    }

    public void ResetStates()
    {
        currentCoins = 0;
        coinsText.text = "Coin : " + currentCoins.ToString();

        level = 1;
        levelText.text = level.ToString();

        engauge = 0;
        GaugeUp();

        for (int i = 0; i < enhanceStates.Length; i++)
        {
            enhanceStates[i].Initialize();
            stateButtons[i].interactable = true;
        }
        for (int i = 0;i < skillStates.Length; i++)
        {
            skillStates[i].Intialize();
            DataManager.Instance.player.skillON[i] = false;
        }
        playerStates.Initialize(); // CharacterStates 초기화

        DataManager.Instance.player.level = level;
        StateSave();
        Debug.Log("<color=lime>[SUCCESS]</color> 스탯 데이터 초기화 완료");
    }

    public void LoadStates()
    {
        currentCoins = DataManager.Instance.player.coins;
        coinsText.text = "Coin : " + currentCoins.ToString();

        level = DataManager.Instance.player.level;
        levelText.text = level.ToString(); 

        engauge = DataManager.Instance.player.engauge;
        GaugeUp();


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
        for (int i = 0; i < skillStates.Length; i++)
        {
            skillStates[i].skillON = DataManager.Instance.player.skillON[i];

            if (skillStates[i].skillON == true)
            {
                Skills[i].interactable = false;
            }
        }

        Debug.Log("<color=lime>[SUCCESS]</color> 스탯 데이터 불러오기 완료");
    }

    public void PickState(int index)
    {
        selectedIndex = index; // 선택된 스탯 인덱스를 저장
        select.SetActive(true); // 구매할래?
    }

    public void Upgrade()
    {
        // 업그레이드 정보
        float cost = enhanceStates[selectedIndex].stateUpgrade[level].cost; // 첫 번째 업그레이드 비용
        string statName = enhanceStates[selectedIndex].stateName; // 스탯 이름

        // 업그레이드 가능 여부 체크
        if (currentCoins >= cost)
        {
            // 스탯 업그레이드
            float increaseAmount = enhanceStates[selectedIndex].stateUpgrade[level].increaseAmount;

            if (enhanceStates[selectedIndex].stateUpgrade[level].isPercentage)
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
            GaugeUp(); // 게이지 올리기
            select.SetActive(false);

            StateSave(); //스탯 저장하고
            UpdateCoinsText(); // 코인 재설정
        }
        else
        {
            ToolTipManager.Instance.TipOn("※ 코인이 <color=orange>부족</color>합니다! ※");
        }
    }

    public void StateSave()
    {
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
    }

    public void UpdateCoinsText()
    {
        coinsText.text = $"Coin : {currentCoins}";
    }

    public void GaugeUp()
    {
        if (engauge == 0)
        {
            for (int i = 0; i < enhanceStates.Length; i++) { gauge[i].GetComponent<Image>().color = Color.white; stateButtons[i].interactable = true; }
        }
        else if (engauge > 0)
        {
            for (int i = 0; i < engauge; i++) { gauge[i].GetComponent<Image>().color = Color.green; }

            if (engauge == 9)
            {
                if (level == 3)
                {
                    Debug.Log("스킬 업글 끝");   
                }
                else
                {
                    // 스킬 열리는 효과
                    Skillinteractable();
                }
            }
        }
    }

    public void Skillinteractable()
    {
        for (int i = 0; i < skillStates.Length; i++)
        {
            Skills[i].interactable = true;

            if (skillStates[i].skillON == true)
            {
                Skills[i].interactable = false;
            }
        }
    }

    public void PickSkill(int index)
    {
        selectedSkillIndex = index; // 선택된 스탯 인덱스를 저장
        selectskill.SetActive(true); // 구매할래?
    }

    public void SkillUpgrade()
    {
        if (skillStates[selectedSkillIndex].skillON == false)
        {
            skillStates[selectedSkillIndex].skillON = true;
            Skills[selectedSkillIndex].interactable = false;

            level += 1;
            levelText.text = level.ToString();
            engauge = 0;
            for (int i = 0; i < enhanceStates.Length; i++) { gauge[i].GetComponent<Image>().color = Color.white; stateButtons[i].interactable = true; enhanceStates[i].isUpgraded = false; }
            DesChange();

            DataManager.Instance.player.level = level;
            DataManager.Instance.player.engauge = engauge;

            DataManager.Instance.player.skillON[selectedSkillIndex] = true;
            DataManager.Instance.Save();

            selectskill.SetActive(false);
            Skills[0].interactable = false;
            Skills[1].interactable = false;
            Skills[2].interactable = false;
        }
    }

    public void DesChange()
    {
        // 어차피 이미지로 바뀔 수도 있으니까 대충 구현
        for (int i = 0; i < enhanceStates.Length; i++)
        {
            switch (i)
            {
                case 0:
                    stateButtons[0].GetComponentInChildren<Text>().text = $"최대 체력\n+{enhanceStates[0].stateUpgrade[level].increaseAmount}\n\n-{enhanceStates[0].stateUpgrade[level].cost}C";
                    break;
                case 1:
                    stateButtons[1].GetComponentInChildren<Text>().text = $"회복\n+{enhanceStates[1].stateUpgrade[level].increaseAmount}\n\n-{enhanceStates[1].stateUpgrade[level].cost}C";
                    break;
                case 2:
                    stateButtons[2].GetComponentInChildren<Text>().text = $"방어력\n+{enhanceStates[2].stateUpgrade[level].increaseAmount}\n\n-{enhanceStates[2].stateUpgrade[level].cost}C";
                    break;
                case 3:
                    stateButtons[3].GetComponentInChildren<Text>().text = $"이동 속도\n+{enhanceStates[3].stateUpgrade[level].increaseAmount}%\n\n-{enhanceStates[3].stateUpgrade[level].cost}C";
                    break;
                case 4:
                    stateButtons[4].GetComponentInChildren<Text>().text = $"피해량\n+{enhanceStates[4].stateUpgrade[level].increaseAmount}%\n\n-{enhanceStates[4].stateUpgrade[level].cost}C";
                    break;
                case 5:
                    stateButtons[5].GetComponentInChildren<Text>().text = $"공격 범위\n+{enhanceStates[5].stateUpgrade[level].increaseAmount}%\n\n-{enhanceStates[5].stateUpgrade[level].cost}C";
                    break;
                case 6:
                    stateButtons[6].GetComponentInChildren<Text>().text = $"쿨타임\n+{enhanceStates[6].stateUpgrade[level].increaseAmount}%\n\n-{enhanceStates[6].stateUpgrade[level].cost}C";
                    break;
                case 7:
                    stateButtons[7].GetComponentInChildren<Text>().text = $"자석\n+{enhanceStates[7].stateUpgrade[level].increaseAmount}\n\n-{enhanceStates[7].stateUpgrade[level].cost}C";
                    break;
                case 8:
                    stateButtons[8].GetComponentInChildren<Text>().text = $"저주\n+{enhanceStates[8].stateUpgrade[level].increaseAmount}\n\n-{enhanceStates[8].stateUpgrade[level].cost}C";
                    break;
                default:
                    break;
            }
        }
    }

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