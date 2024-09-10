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

    private void Start()
    {
        Debug.Log("[E] Skill_0 사용 임시 설정 키");
        Debug.Log("[R] Skill_1 사용 임시 설정 키");

        // 쿨 타임 나타내는 타임바 0으로 설정
        skill0Cool.fillAmount = 0;
        skill1Cool.fillAmount = 0;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Skill0"))
        {
            Skill_0();
        }
        if (Input.GetButtonDown("Skill1"))
        {
            Skill_1();
        }
    }

    // Skill 0 ()
    private void Skill_0()
    {
        Debug.Log("skill_0");
        StartCoroutine(Cooltime());
    }

    // Skill 1 ()
    private void Skill_1()
    {
        Debug.Log("skill_1");
    }

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
