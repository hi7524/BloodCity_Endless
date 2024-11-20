using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyBtn : MonoBehaviour
{
    public Button MainBtn;
    public Button PauseBtn;
    public Button StageBtn;
    public Button ChaBtn;
    public Button EquipBtn;

    public GameObject mainWindow; // 메인 창
    public GameObject pauseWindow; // 옵션 창
    public GameObject StageSelect; // 스테이지 선택 창
    public GameObject chaWindow; // 캐릭터 창
    public GameObject equipWindow; // 장비 창

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (mainWindow.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(MainBtn.gameObject);
            }
            if (pauseWindow.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(PauseBtn.gameObject);
            }
            if (StageSelect.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(StageBtn.gameObject);
            }
            if (chaWindow.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(ChaBtn.gameObject);
            }
            if (equipWindow.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(EquipBtn.gameObject);
            }
        }
    }
}
