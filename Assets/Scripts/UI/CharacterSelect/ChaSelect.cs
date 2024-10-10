using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChaSelect : MonoBehaviour
{
    public static ChaSelect Instance { get; private set; }

    [Header("Image")]
    public Sprite[] character = new Sprite[3]; // 캐릭터 이미지 배열
    public Image[] ChaList = new Image[3]; // 캐릭터 슬롯 오브젝트 배열

    [Header("Des")]
    public TMP_Text chaDescription; // 캐릭터 설명 텍스트

    [Header("Stat")]
    public TMP_Text chaState; // 캐릭터 스탯 텍스트
    public ChaStateManager[] characterStates = new ChaStateManager[3]; // 캐릭터 스탯 배열

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

        check.SetActive(false);
    }

    public void PermanentStat()
    {
        // 강화된 기본 스탯들
        characterStates[currentIndex].InitializeStats();
        chaState.text = characterStates[currentIndex].GetStats();

        // 도전할 스테이지 번호

        //currentIndex = DataManager.Instance.player.currentIndex;
    }

    private void OnEnable()
    {
        PermanentStat(); // 영구강화 데이터 받아오기
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
                Debug.LogError($"<color=orange>[WARNING]</color> ChaList[{i}]는 null입니다.");
            }
        }
    }

    public void ShowNextCharacter(string LR)
    {
        // 현재 인덱스를 업데이트
        if (LR == "L")
        {
            //Debug.Log("왼쪽으로 캐릭터를 넘겼습니다.");
            currentIndex = (currentIndex - 1 + character.Length) % character.Length;
        }
        else if (LR == "R")
        {
            //Debug.Log("오른쪽으로 캐릭터를 넘겼습니다.");
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
                Debug.LogError($"<color=orange>[WARNING]</color> 슬롯 또는 캐릭터 이미지가 null입니다.");
            }
        }

        // 캐릭터 설명과 스탯 업데이트
        UpdateCharacterInfo();
    }

    private void UpdateCharacterInfo()
    {
        if (chaDescription != null && chaState != null)
        {
            chaDescription.text = characterStates[currentIndex].bonusStates.characterDetail;
            characterStates[currentIndex].InitializeStats();
            chaState.text = characterStates[currentIndex].GetStats(); // 스탯 업데이트
        }
        else
        {
            Debug.LogError("<color=orange>[WARNING]</color> chaDescription 또는 chaState가 null입니다.");
        }
    }

    public void ChaSave()
    {
        DataManager.Instance.player.currentIndex = currentIndex;
        DataManager.Instance.Save();

        // 캐릭터 인덱스에 따라 스트링값 넣고 스테이지 값 나중에 불러오고
        check_text.text = $"현재 선택한 캐릭터 : ㅇㅇㅇㅇ" +
            $"\r\n이번 필드 : ㅁㅁㅁㅁ?" +
            "\r\n\r\n게임을 시작하시겠습니까?"; ;

        check.SetActive(true);
    }

    public void CheckOff()
    {
        check.SetActive(false);
    }
}