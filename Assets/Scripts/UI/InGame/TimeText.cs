using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    public static TimeText Instance;

    public Text time;

    public float sec;

    public int minutes;
    public float seconds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        sec = 0f;
    }

    void Update()
    {
        sec = TimeManager.Instance.nowTime;

        // 분과 초 계산
        minutes = Mathf.FloorToInt(sec / 60);
        seconds = sec % 60;

        if (seconds >= 59)
        {
            seconds = 59;
        }

        // 타이머를 화면에 표시
        time.text = $"{minutes:00} : {seconds:00}";
    }
}
