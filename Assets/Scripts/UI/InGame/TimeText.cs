using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    public Text time;

    int min;
    float sec;

    void Update()
    {
        min = TimeManager.Instance.nowMin;
        sec = TimeManager.Instance.nowTime;
        
        if (TimeManager.Instance.nowTime > 60)
        {
            sec -= 60;
        }

        time.text = min.ToString("00") + " : " + sec.ToString("00");
    }
}
