using UnityEngine;
using UnityEngine.UI;

public class KillText : MonoBehaviour
{
    public static KillText Instance;

    public Text killText;

    public int kill;

    private void Awake()
    {
        if (Instance == null) { Instance = this; } 
    }

    public void KillUP()
    {
        kill++;
        killText.text = $"{kill:#,0}";
    }
}
