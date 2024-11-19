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
        Time.timeScale = 0;
        resultWindow.SetActive(true);
        Result();
    }

    // 결과창에 떠야 하는 것
    public void Result()
    {
        result.text = $"생존 시간 :                {TimeManager.Instance.nowMin}:{TimeText.Instance.sec}";
        // 생존 시간
        // 플레이어 레벨
        // 처치한 적
        // 획득한 코인

        // 선택한 캐릭터 이름
        // 획득한 무기 (레벨업 상태?)

        // 레벨업 강화 (...스탯 상태?)

        BackBtn.SetActive(true);
    }

    public void Back()
    {
        // 등등 저장해야하는 것들
        // DataManager.Instance.player.coins = GameManager.Instance.coin;
        // DataManager.Instance.player.PlayerRestart = true;
        // DataManager.Instance.Save();
        Debug.Log("마지막에 수정예정");
        Debug.Log("타이머 연동과 처치수 연동 필요");
        SceneManager.LoadScene("StartScene");
    }
}
