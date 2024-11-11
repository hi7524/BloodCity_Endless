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
    public Image[] stateCoin; // 스탯 가격 배열
    public int selectedIndex = 0; // 선택된 스탯 인덱스

    [Header("LevelGauge")]
    public int level = 1; // 현재 단계
    public int engauge; // 강화 횟수

    [Header("Skill")]
    public SkillStates[] skillStates; // 스킬 스탯 배열
    public Button[] Skills; // 스킬 버튼
    public Image[] skillRock; // 스킬 잠금
    public int selectedSkillIndex = 0; // 선택된 스킬 인덱스

    [Header("Tap")]
    public GameObject select;
    public GameObject selectskill;

    private string[] state = { "maxHealth", "restorePerSec", "defense", "speed", "attackDamage", "attackRange" , "abilityHaste", "magnetism", "curse" };

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
    }

    public void UpdateCoinsText()
    {
        coinsText.text = $"{currentCoins:#,0}";
    }

    public void ResetStates()
    {
        currentCoins = 0;
        UpdateCoinsText();
        level = 1;
        engauge = 0;

        playerStates.Initialize(); // CharacterStates 초기화

        for (int i = 0; i < enhanceStates.Length; i++)
        {
            enhanceStates[i].Initialize(); // 인챈트 스탯 초기화
            stateButtons[i].interactable = true; // 스탯 클릭 가능
            stateCoin[i].gameObject.SetActive(true);
        }

        for (int i = 0;i < skillStates.Length; i++)
        {
            skillStates[i].Intialize(); // 스킬 획득 초기화
            skillRock[i].gameObject.SetActive(true);
            Skills[i].interactable = false;
            DataManager.Instance.player.skillON[i] = false; // 데이터도 초기화
        }

        DataManager.Instance.player.level = level;
        Change();
        StateSave();

        Debug.Log("<color=lime>[SUCCESS]</color> 스탯 데이터 초기화 완료");
    }

    public void LoadStates()
    {
        currentCoins = DataManager.Instance.player.coins;
        UpdateCoinsText();

        level = DataManager.Instance.player.level;
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
                stateCoin[i].gameObject.SetActive(false);
            }

            Change();
        }

        for (int i = 0; i < skillStates.Length; i++)
        {
            skillStates[i].skillON = DataManager.Instance.player.skillON[i];
            Skills[i].interactable = false;
            
            if (skillStates[i].skillON == true)
            {
                skillRock[i].gameObject.SetActive(false);
            }

            if (engauge == 9)
            {
                Skills[i].interactable = true;
                skillRock[i].gameObject.SetActive(true);
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
            stateCoin[selectedIndex].gameObject.SetActive(false);

            engauge += 1;
            if (engauge == 9)
            {
                Skillinteractable();
                ToolTipManager.Instance.TipOn("※ 스킬이 <color=lime>해금</color> 되었습니다! ※");
            }

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

    public void Skillinteractable()
    {
        for (int i = 0; i < skillStates.Length; i++)
        {
            Skills[i].interactable = true;
            skillRock[i].sprite = Resources.Load<Sprite>("EnStates/select");

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
            skillRock[selectedSkillIndex].gameObject.SetActive(false);

            selectskill.SetActive(false);

            for (int i = 0; i < skillStates.Length; i++)
            {
                Skills[i].interactable = false;
                skillRock[i].sprite = Resources.Load<Sprite>("EnStates/rock");

                if (skillStates[i].skillON == true)
                {
                    skillRock[i].gameObject.SetActive(false);
                }
            }

            if (level < 3)
            {
                level += 1;
                DesChange(); // 카드 이미지 바꾸기
                DataManager.Instance.player.level = level;
            }
            else if (level == 3)
            {
                ToolTipManager.Instance.TipOn("※ <color=lime>[SUCCESS]</color> 영구 강화 완료! ※");
            }

            engauge = 0;
            DataManager.Instance.player.engauge = engauge;
            DataManager.Instance.player.skillON[selectedSkillIndex] = true;
            DataManager.Instance.Save();
        }
    }

    public void DesChange()
    {
        for (int i = 0; i < enhanceStates.Length; i++)
        {
            Change();
            stateButtons[i].interactable = true;
            stateCoin[i].gameObject.SetActive(true);
            enhanceStates[i].isUpgraded = false;
            DataManager.Instance.player.isUpgraded[i] = enhanceStates[i].isUpgraded;
            DataManager.Instance.Save();
        }
    }

    public void Change()
    {
        for (int i = 0; i < enhanceStates.Length; i++)
        {
            Sprite nextState;
            Sprite nextCoin;

            nextState = Resources.Load<Sprite>("EnStates/" + state[i] + level);
            nextCoin = Resources.Load<Sprite>("EnStates/Coin" + level);
            stateButtons[i].image.sprite = nextState;
            stateCoin[i].sprite = nextCoin;
        }
    }
}