using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// UI Manager
public class UI_StartScene : MonoBehaviour
{
    [SerializeField] GameObject chaWindow; // ĳ���� â
    [SerializeField] GameObject equipWindow; // ��� â

    private void Start()
    {
        // �ʱ� ����
        chaWindow.SetActive(false);
        equipWindow.SetActive(false);
    }

    public void GameStartBtn()
    {
        //��ư ȿ����
        chaWindow.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void EquipBtn()
    {
        // ��ư ȿ����
        equipWindow.SetActive(true);
        Time.timeScale = 0.0f;
    }

    // ������� ĳ���� ����â ��ư





    public void GoStage()
    {
        SceneManager.LoadScene("GameScene");
    }

    // ������� ���� ��ȭ â ��ư
}