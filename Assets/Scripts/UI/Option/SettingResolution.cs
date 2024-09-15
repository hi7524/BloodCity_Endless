using UnityEngine;
using System.IO;
using TMPro;

public class SettingResolution : MonoBehaviour
{
    public static SettingResolution Instance { get; private set; }

    public TMP_Dropdown dropdown;                                       // 해상도 드롭다운
    public TMP_Dropdown full;                                           // 전체화면 드롭다운

    [Header("Real")]
    public int RealWidth;                                               // 디바이스의 원래 너비
    public int RealHeight;                                              // 디바이스의 원래 높이

    [Header("Alter")]
    public bool fullScreen;                                             // 전체 화면이니?

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
            Debug.Log("플레이어 데이터가 존재합니다.");
            fullScreen = DataManager.Instance.player.fullScreen;
            dropdown.value = DataManager.Instance.player.dropdownValue;
            full.value = DataManager.Instance.player.fullValue;

            Debug.Log("플레이어 데이터 동기화 완료.");

            SetResolution();
            FullScreen();
        }
        else
        {
            Debug.Log("플레이어 데이터가 아직 존재하지 않습니다.");
            Screen.SetResolution(1920, 1080, false);
        }
    }

    private void InitializeResolution()
    {
        Resolution currentResolution = Screen.currentResolution;
        RealWidth = currentResolution.width;
        RealHeight = currentResolution.height;
        Debug.Log($"이 디바이스의 전체 화면은 {RealWidth} X {RealHeight}");
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
        Debug.Log($"지금 화면은 {Screen.width} X {Screen.height}");
    }

    public void FullScreen()
    {
        switch (full.value)
        {
            case 0: // 창모드
                SetWindowedMode();
                break;
            case 1: // 전체화면 창모드
                SetFullWindow();
                break;
            case 2: // 전체화면
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
        dropdown.interactable = true;                                   // 해상도 조절 가능하게
        fullScreen = false;
        Debug.Log("창모드 입니다");
    }

    private void SetFullWindow()
    {
        Screen.SetResolution(RealWidth, RealHeight - 30, false);
        dropdown.interactable = false;                                  // 해상도 조절 불가능
        fullScreen = false;
        Debug.Log("전체화면 창모드 입니다");
    }

    private void SetFullScreenMode()
    {
        Screen.SetResolution(RealWidth, RealHeight, true);
        dropdown.interactable = false;                                  // 해상도 조절 불가능
        fullScreen = true;
        Debug.Log("전체화면 입니다");
    }
}