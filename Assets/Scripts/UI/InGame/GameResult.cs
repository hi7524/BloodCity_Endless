using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameResult : MonoBehaviour
{
    public GameObject resultWindow;
    public GameObject BackBtn;

    public Text result;

    public void Start()
    {
        resultWindow.SetActive(false);
    }

    // 승리든 패배든 확인 누르면
    public void ResultOn()
    {
        // 끄고
        UIManager.Instance.ToggleWindow(UIManager.Instance.gameEndWindow);
        UIManager.Instance.ToggleWindow(UIManager.Instance.mainWindow);
        Time.timeScale = 0;
        resultWindow.SetActive(true);
        Result();
    }

    // 결과창에 떠야 하는 것
    public void Result()
    {
        result.text = $"생존 시간 :                {TimeManager.Instance.nowMin}:{TimeText.Instance.sec.ToString("f0")}\n" +
            $"도달한 레벨 :             {PlayerLevel.Instance.playerLevel} Lv\n" +
            $"처치한 적 :         {KillText.Instance.kill} 마리\n" +
            $"획득한 코인 :           {GameManager.Instance.coin} G\n";

        BackBtn.SetActive(true);
    }

    public void Back()
    {
        // 등등 저장해야하는 것들
        DataManager.Instance.player.coins += GameManager.Instance.coin;
        DataManager.Instance.player.PlayerRestart = true;
        DataManager.Instance.Save();

        SceneManager.LoadScene("StartScene");
    }
}
