using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    public static TimeText Instance;

    public Text time;

    int min;
    public float sec;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        min = TimeManager.Instance.nowMin;
        sec = TimeManager.Instance.nowTime;

        // 60 이상일 경우
        if (sec >= 60)
        {
            // 몇 분이 경과했는지 계산
            int minutesPassed = Mathf.FloorToInt(sec / 60);
            sec -= minutesPassed * 60; // 경과한 분에 해당하는 초를 빼기
        }

        time.text = min.ToString("00") + " : " + sec.ToString("00");
    }
}
