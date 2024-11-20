using UnityEngine;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager Instance { get; private set; }

    public GameObject toolTip;
    public Text toolTipText;

    public Animator ani;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // 다른 어디서든 툴팁 안에 넣을 텍스트로 함수를 부르면 툴팁을 띄울 수 있음
    public void TipOn(string text)
    {
        toolTip.SetActive(true);
        toolTipText.text = text;
        ani.Play("ToolTip");
        Invoke(nameof(TipOff), 2.5f);
    }

    public void TipOff()
    {
        toolTip.SetActive(false);
    }
}
