using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 들고 다닐 UI Manager
public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject pauseWindow; // 옵션 창

    private void Start()
    {
        // 초기 설정
        pauseWindow.SetActive(false); // 옵션 창 비활성화
    }

    private void Update()
    {
        // ESC키를 누를시 일시정지
        if (Input.GetButtonDown("ESC"))
        {
            // 효과음?
            Pause();
        }
    }

    // 게임 일시정지
    public void Pause()
    {
        // 창 끄기
        if (pauseWindow.activeSelf)
        {
            // 효과음?
            pauseWindow.SetActive(false);
            Time.timeScale = 1.0f;
        }
        // 창 켜기
        else
        {
            // 버튼 효과음
            pauseWindow.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    // 게임 종료
    public void Exit()
    {
        Application.Quit();
    }
}
