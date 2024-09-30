using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkillSystem : MonoBehaviour
{
    //public GameObject[] skills;
    public List<GameObject> skills;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 장착 무기 추가
        IPlayerSkill skill = collision.GetComponent<IPlayerSkill>();

        if (skill != null)
        {
            skills.Add(collision.gameObject);
            //Destroy(collision.gameObject);
        }
    }
    private void Update()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            Debug.Log("스킬사용함");
            skills[i].GetComponent<IPlayerSkill>().UseSkill();
            skills[i].GetComponent<IPlayerSkill>().playerVec = transform.position;
           
        }
    }

    // 무기 획득시 활성화 하기
    // IPlayerSkill.isSkillActive = true;
}
