using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class CharacterStats
{
    // 기본 스탯
    public float maxHealth = 100;    // 최대 체력
    public float restorePerSec = 0; // 초당 회복량
    public float defense = 1;        // 방어력
    public float speed = 100;        // 이동 속도 (%)

    public float attackDamage = 100; // 공격력 (%)
    public float attackRange = 100;   // 공격 범위 (%)

    public float abilityHaste = 0;  // 능력 가속 (쿨감, %)
    public float magnetism = 0;       // 자성
    public float curse = 0;           // 저주

    // 보너스 스탯
    public float maxHealthBonus = 0;    // 최대 체력 보너스
    public float restorePerSecBonus = 0; // 초당 회복량 보너스
    public float defenseBonus = 0;        // 방어력 보너스
    public float speedBonus = 0;          // 이동 속도 보너스

    public float attackDamageBonus = 0; // 공격력 보너스
    public float attackRangeBonus = 0;   // 공격 범위 보너스

    public float abilityHasteBonus = 0;  // 능력 가속 보너스
    public float magnetismBonus = 0;      // 자성 보너스
    public float curseBonus = 0;          // 저주 보너스

    // 스탯을 문자열로 반환하는 메서드
    public string GetStats()
    {
        string stats = $"최대 체력: {maxHealth}";
        if (maxHealthBonus != 0)
        {
            stats += $" <color=red>+ {maxHealthBonus}</color>";
        }
        stats += $"\n초당 회복량: {restorePerSec}";
        if (restorePerSecBonus != 0)
        {
            stats += $" <color=red>+ {restorePerSecBonus}</color>";
        }
        stats += $"\n방어력: {defense}";
        if (defenseBonus != 0)
        {
            stats += $" <color=red>+ {defenseBonus}</color>";
        }
        stats += $"\n이동 속도: {speed}";
        if (speedBonus != 0)
        {
            stats += $" <color=red>+ {speedBonus}</color>";
        }
        stats += $"\n\n공격력: {attackDamage}";
        if (attackDamageBonus != 0)
        {
            stats += $" <color=red>+ {attackDamageBonus}</color>";
        }
        stats += $"\n공격 범위: {attackRange}";
        if (attackRangeBonus != 0)
        {
            stats += $" <color=red>+ {attackRangeBonus}</color>";
        }
        stats += $"\n\n능력 가속: {abilityHaste}";
        if (abilityHasteBonus != 0)
        {
            stats += $" <color=red>+ {abilityHasteBonus}</color>";
        }
        stats += $"\n자성: {magnetism}";
        if (magnetismBonus != 0)
        {
            stats += $" <color=red>+ {magnetismBonus}</color>";
        }
        stats += $"\n저주: {curse}";
        if (curseBonus != 0)
        {
            stats += $" <color=red>+ {curseBonus}</color>";
        }

        return stats;
    }
}

public class ChaSelect : MonoBehaviour
{
    public static ChaSelect Instance { get; private set; }

    [Header("Image")]
    public Sprite[] character = new Sprite[5]; // 캐릭터 이미지 배열
    public Image[] ChaList = new Image[5]; // 캐릭터 슬롯 오브젝트 배열

    [Header("Des")]
    public TMP_Text chaDescription; // 캐릭터 설명 텍스트
    public string[] characterDescriptions = new string[5]; // 캐릭터 설명 배열

    [Header("Stat")]
    public TMP_Text chaState; // 캐릭터 스탯 텍스트
    public CharacterStats[] characterStats = new CharacterStats[5]; // 캐릭터 스탯 배열

    [Header("Etc.")]
    public Button LBtn;
    public Button RBtn;
    public int currentIndex = 0; // 현재 캐릭터 인덱스 번호

    [Header("check")]
    public GameObject check;
    public TMP_Text check_text;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        // 창 꺼지니까 할당이 풀려서 임시조치
        ChaList[0] = GameObject.Find("Cha1").GetComponent<Image>();
        ChaList[1] = GameObject.Find("Cha2").GetComponent<Image>();
        ChaList[2] = GameObject.Find("Cha3").GetComponent<Image>();
        ChaList[3] = GameObject.Find("Cha4").GetComponent<Image>();
        ChaList[4] = GameObject.Find("Cha5").GetComponent<Image>();

        // 캐릭터 설명 넣기
        characterDescriptions[0] = "우주 해병은 잘 훈련된 엘리트 집단입니다. \r\n기본적인 전투 훈련을 받아 강인한 체력을 가지고 있고 무기 조작에 능합니다.";
        characterDescriptions[1] = "2번\r\n대충설명";
        characterDescriptions[2] = "3번\r\n대충설명";
        characterDescriptions[3] = "4번\r\n대충설명";
        characterDescriptions[4] = "5번\r\n대충설명";

        check.gameObject.SetActive(false);
    }

    // 영구 강화로 강화된 스탯에 캐릭터 보정치를 따로 더하려고 이렇게 만들었는데 나중에 바꾸던지 합시다...
    public void PermanentStat()
    {
        // 강화된 기본 스탯들

        // 도전할 스테이지 번호

        currentIndex = DataManager.Instance.player.currentIndex;
    }

    private void OnEnable()
    {
        AssignImagesToSlots(); // 슬롯에 이미지를 할당
        UpdateCharacterDisplay(); // 초기 디스플레이 업데이트
    }

    private void AssignImagesToSlots()
    {
        // ChaList 배열에 캐릭터 이미지를 할당
        for (int i = 0; i < ChaList.Length; i++)
        {
            if (ChaList[i] != null && character.Length > 0)
            {
                int index = (currentIndex + i) % character.Length;
                ChaList[i].sprite = character[index];
            }
            else
            {
                Debug.LogError($"ChaList[{i}]는 null입니다.");
            }
        }
    }

    public void ShowNextCharacter(string LR)
    {
        // 현재 인덱스를 업데이트
        if (LR == "L")
        {
            Debug.Log("왼쪽으로 캐릭터를 넘겼습니다.");
            currentIndex = (currentIndex - 1 + character.Length) % character.Length;
        }
        else if (LR == "R")
        {
            Debug.Log("오른쪽으로 캐릭터를 넘겼습니다.");
            currentIndex = (currentIndex + 1) % character.Length;
        }

        UpdateCharacterDisplay();
    }

    private void UpdateCharacterDisplay()
    {
        // 슬롯에 이미지를 업데이트
        for (int i = 0; i < ChaList.Length; i++)
        {
            if (ChaList[i] != null && character.Length > 0)
            {
                int index = (currentIndex + i) % character.Length;
                ChaList[i].sprite = character[index];
            }
            else
            {
                Debug.LogError($"슬롯 또는 캐릭터 이미지가 null입니다.");
            }
        }

        // 캐릭터 설명과 스탯 업데이트
        UpdateCharacterInfo();
    }

    private void UpdateCharacterInfo()
    {
        if (chaDescription != null && chaState != null)
        {
            chaDescription.text = characterDescriptions[currentIndex];
            chaState.text = characterStats[currentIndex].GetStats(); // 스탯 업데이트
        }
        else
        {
            Debug.LogError("chaDescription 또는 chaState가 null입니다.");
        }
    }

    public void ChaSave()
    {
        DataManager.Instance.player.currentIndex = currentIndex;
        DataManager.Instance.Save();

        // 캐릭터 인덱스에 따라 스트링값 넣고 스테이지 값 나중에 불러오고
        check_text.text = $"현재 선택한 캐릭터 : 우주해병" +
            $"\r\n도전 할 스테이지 : 스테이지 1" +
            "\r\n\r\n게임을 시작하시겠습니까?"; ;

        check.gameObject.SetActive(true);
    }

    public void CheckOff()
    {
        check.gameObject.SetActive(false);
    }
}