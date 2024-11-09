using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChaSelect : MonoBehaviour
{
    public static ChaSelect Instance { get; private set; }

    [Header("Image")]
    public Sprite[] character = new Sprite[3]; // 캐릭터 이미지 배열
    public GameObject ChaWindow;

    [Header("Stat")]
    public TMP_Text[] chaState = new TMP_Text[9]; // 캐릭터 스탯 텍스트 배열
    public ChaStateManager[] characterStates = new ChaStateManager[3]; // 캐릭터 스탯 배열

    [Header("Etc.")]
    public Button LBtn;
    public Button RBtn;
    public int currentIndex = 0; // 현재 캐릭터 인덱스 번호

    [Header("check")]
    public GameObject check;
    public TMP_Text check_text;

    string cha;
    string sta;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        check.SetActive(false);
    }

    public void PermanentStat()
    {
        // 강화된 기본 스탯들
        characterStates[currentIndex].InitializeStats();
        characterStates[currentIndex].GetStats();
        ChaWindow.GetComponent<Image>().sprite = character[currentIndex];
    }

    private void Start()
    {
        PermanentStat();
        UpdateCharacterInfo();
    }

    public void ShowNextCharacter(string LR)
    {
        // 현재 인덱스를 업데이트
        if (LR == "L")
        {
            currentIndex = (currentIndex - 1 + character.Length) % character.Length;
            PermanentStat();
        }
        else if (LR == "R")
        {
            currentIndex = (currentIndex + 1) % character.Length;
            PermanentStat();
        }
        UpdateCharacterInfo();
    }

    public void UpdateCharacterInfo()
    {
        for (int i = 0; i < chaState.Length; i++)
        {
            chaState[i].color = Color.white;

            if (currentIndex == 0)
            {
                chaState[0].color = Color.green;
                chaState[6].color = Color.green;
            }
            else if (currentIndex == 1)
            {
                chaState[1].text = "<color=red>-</color>";
                chaState[2].color = Color.green;
                chaState[4].color = Color.green;
            }
            else if (currentIndex == 2)
            {
                chaState[1].color = Color.green;
                chaState[3].color = Color.green;
                chaState[5].color = Color.green;
            }

            chaState[i].text = characterStates[currentIndex].stateList[i].ToString();
        }
    }

    public void ChaSave()
    {
        DataManager.Instance.player.currentIndex = currentIndex;
        DataManager.Instance.Save();

        switch (currentIndex) {
            case 0:
                cha = "우주해병";
                break;
            case 1:
                cha = "Beeper";
                break;
            case 2:
                cha = "바즈";
                break;
            default:
                cha = "알 수 없는 캐릭터";
                break;
        }

        switch (DataManager.Instance.player.stageIndex)
        {
            case 0:
                sta = "Stage 1";
                break;
            case 1:
                sta = "Stage 2";
                break;
            case 2:
                sta = "Stage 3";
                break;
            default:
                sta = "알 수 없는 스테이지";
                break;
        }

        // 캐릭터 인덱스에 따라 스트링값 넣고 스테이지 값 나중에 불러오고
        check_text.text = $"현재 선택한 캐릭터 : " + cha +
            $"\r\n이번 필드 : " + sta +
            "\r\n\r\n게임을 시작하시겠습니까?"; ;

        check.SetActive(true);
    }

    public void CheckOff()
    {
        check.SetActive(false);
    }
}