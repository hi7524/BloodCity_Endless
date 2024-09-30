using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSystem : MonoBehaviour
{
    public GameObject[] skills;

    private void Start()
    {
        // 스킬 활성화 (임시)
        for (int i = 0; i < skills.Length; i++)
        {
           
            skills[i].GetComponent<IPlayerSkill>().isSkillActive = true;
        }

    }

    private void Update()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].GetComponent<IPlayerSkill>().UseSkill();
        }
    }

    // 무기 획득시 활성화 하기
    // IPlayerSkill.isSkillActive = true;
}
