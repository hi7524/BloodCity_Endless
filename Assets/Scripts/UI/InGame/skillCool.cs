using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class skillCool : MonoBehaviour
{
    public static skillCool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Image cooltimeImg;
    public Image cooltimeImg2;

    public IEnumerator Cool(float skillcool, float skillcool_max)
    {
        while (skillcool > 0.0f)
        {
            skillcool -= Time.deltaTime;

            if (skillcool_max == 5) { cooltimeImg.fillAmount = skillcool / skillcool_max; }
            if (skillcool_max == 10) { cooltimeImg2.fillAmount = skillcool / skillcool_max; }
            

            yield return new WaitForFixedUpdate();
        }
    }
}
