using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartScene : MonoBehaviour
{
    [SerializeField] GameObject mainWindow; // 메인 창
    [SerializeField] GameObject pauseWindow; // 옵션 창
    [SerializeField] GameObject chaWindow; // 캐릭터 창
    [SerializeField] GameObject equipWindow; // 장비 창

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
            Time.timeScale = 1.0f;
        }
        // 창 켜기
        else
        {
            Return();
            window.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    // 캐릭터 선택창 버튼
    public void GoStage()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1.0f;
    }

    // 초기 상태
    public void Return()
    {
        mainWindow.SetActive(false);
        chaWindow.SetActive(false);
        pauseWindow.SetActive(false);
        equipWindow.SetActive(false);
    }

    // 게임 종료
    public void Exit()
    {
        Application.Quit();
    }
}