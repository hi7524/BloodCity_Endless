using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// UI Manager
public class UI_StartScene : MonoBehaviour
{
    [SerializeField] GameObject chaWindow; // 캐릭터 창
    [SerializeField] GameObject equipWindow; // 장비 창

    private void Start()
    {
        Return();
    }

    public void GameStartBtn()
    {
        //버튼 효과음
        chaWindow.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void EquipBtn()
    {
        // 버튼 효과음
        equipWindow.SetActive(true);
        Time.timeScale = 0.0f;
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
        equipWindow.SetActive(false);
        Time.timeScale = 1.0f;
    }
}