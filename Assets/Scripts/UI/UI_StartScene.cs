using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartScene : MonoBehaviour
{
    [SerializeField] GameObject pauseWindow; // 옵션 창
    [SerializeField] GameObject chaWindow; // 캐릭터 창
    [SerializeField] GameObject equipWindow; // 장비 창

    private void Start()
    {
        Return();
    }

    public void ToggleWindow(GameObject window)
    {
        // 창 끄기
        if (window.activeSelf)
        {
            window.SetActive(false);
            Time.timeScale = 1.0f;
        }
        // 창 켜기
        else
        {
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
        chaWindow.SetActive(false);
        pauseWindow.SetActive(false);
        equipWindow.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // 게임 종료
    public void Exit()
    {
        Application.Quit();
    }
}