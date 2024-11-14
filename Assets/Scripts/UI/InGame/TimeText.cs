using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    public Text time;

    void Update()
    {
        time.text = TimeManager.Instance.nowMin.ToString("00") + " : " + TimeManager.Instance.nowTime.ToString("00");
    }
}
