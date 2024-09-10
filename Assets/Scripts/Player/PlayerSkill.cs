using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 플레이어의 스킬 사용 및 관리
public class PlayerSkill : MonoBehaviour
{
    [SerializeField] float skill0CoolTime;
    [SerializeField] float skill1CoolTime;

    [SerializeField] Image skill0Cool;
    [SerializeField] Image skill1Cool;



    IEnumerator Cooltime() 
    {
        skill0Cool.fillAmount = 1;

        while (skill0Cool.fillAmount > 0) 
        {
            skill0Cool.fillAmount -= 1 * Time.smoothDeltaTime / skill0CoolTime; 
            yield return null; 
        } 
        yield break; 
    }
   
}
