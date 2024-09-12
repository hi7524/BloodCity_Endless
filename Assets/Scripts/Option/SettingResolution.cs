using UnityEngine;
using System.IO;
using TMPro;

public class SettingResolution : MonoBehaviour
{
    public static SettingResolution Instance { get; private set; }

    public TMP_Dropdown dropdown;                                       // �ػ� ��Ӵٿ�
    public TMP_Dropdown full;                                           // ��üȭ�� ��Ӵٿ�

    [Header("Real")]
    public int RealWidth;                                               // ����̽��� ���� �ʺ�
    public int RealHeight;                                              // ����̽��� ���� ����

    [Header("Alter")]
    public bool fullScreen;                                             // ��ü ȭ���̴�?

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        InitializeResolution();
    }

    public void ScreenData()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            Debug.Log("�÷��̾� �����Ͱ� �����մϴ�.");
            fullScreen = DataManager.Instance.player.fullScreen;
            dropdown.value = DataManager.Instance.player.dropdownValue;
            full.value = DataManager.Instance.player.fullValue;

            Debug.Log("�÷��̾� ������ ����ȭ �Ϸ�.");

            SetResolution();
            FullScreen();
        }
        else
        {
            Debug.Log("�÷��̾� �����Ͱ� ���� �������� �ʽ��ϴ�.");
            Screen.SetResolution(1920, 1080, false);
        }
    }

    private void InitializeResolution()
    {
        Resolution currentResolution = Screen.currentResolution;
        RealWidth = currentResolution.width;
        RealHeight = currentResolution.height;
        Debug.Log($"�� ����̽��� ��ü ȭ���� {RealWidth} X {RealHeight}");
    }

    public void SetResolution()
    {
        switch (dropdown.value)
        {
            case 0:
                SetScreenResolution(1920, 1080);
                break;
            case 1:
                SetScreenResolution(1680, 1050);
                break;
            case 2:
                SetScreenResolution(1280, 720);
                break;
        }

        DataManager.Instance.player.dropdownValue = dropdown.value;
        DataManager.Instance.Save();
    }

    private void SetScreenResolution(int width, int height)
    {
        Screen.SetResolution(width, height, false);
        Debug.Log($"���� ȭ���� {Screen.width} X {Screen.height}");
    }

    public void FullScreen()
    {
        switch (full.value)
        {
            case 0: // â���
                SetWindowedMode();
                break;
            case 1: // ��üȭ�� â���
                SetFullWindow();
                break;
            case 2: // ��üȭ��
                SetFullScreenMode();
                break;
        }

        DataManager.Instance.player.fullScreen = fullScreen;
        DataManager.Instance.player.fullValue = full.value;
        DataManager.Instance.Save();
    }

    private void SetWindowedMode()
    {
        SetResolution();
        dropdown.interactable = true;                                   // �ػ� ���� �����ϰ�
        fullScreen = false;
        Debug.Log("â��� �Դϴ�");
    }

    private void SetFullWindow()
    {
        Screen.SetResolution(RealWidth, RealHeight - 30, false);
        dropdown.interactable = false;                                  // �ػ� ���� �Ұ���
        fullScreen = false;
        Debug.Log("��üȭ�� â��� �Դϴ�");
    }

    private void SetFullScreenMode()
    {
        Screen.SetResolution(RealWidth, RealHeight, true);
        dropdown.interactable = false;                                  // �ػ� ���� �Ұ���
        fullScreen = true;
        Debug.Log("��üȭ�� �Դϴ�");
    }
}