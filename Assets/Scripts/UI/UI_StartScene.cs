using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartScene : MonoBehaviour
{
    public static UI_StartScene Instance { get; private set; }

    [SerializeField] public GameObject mainWindow; // 메인 창
    [SerializeField] public GameObject pauseWindow; // 옵션 창
    [SerializeField] public GameObject StageSelect; // 스테이지 선택 창
    [SerializeField] public GameObject chaWindow; // 캐릭터 창
    [SerializeField] public GameObject equipWindow; // 장비 창

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Return();
        mainWindow.SetActive(true);
    }

    public void ToggleWindow(GameObject window)
    {
        // 창 끄기
        if (window.activeSelf)
        {
            mainWindow.SetActive(true);
            window.SetActive(false);
        }
        // 창 켜기
        else
        {
            Return();
            window.SetActive(true);
        }
    }

    // 캐릭터 선택창 버튼
    public void GoStage()
    {
        SceneManager.LoadScene("PlayerTestScene");
        Time.timeScale = 1.0f;
    }

    // 초기 상태
    public void Return()
    {
        mainWindow.SetActive(false);
        chaWindow.SetActive(false);
        pauseWindow.SetActive(false);
        StageSelect.SetActive(false);
        equipWindow.SetActive(false);
    }

    // 게임 종료
    public void Exit()
    {
        Application.Quit();
    }
}