using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSystem : MonoBehaviour
{
    [Header("기본 공격 스킬")]
    public GameObject defalutSkill; // 기본 공격 스킬 (샷건)
    [Header("패시브 스킬")]
    public List<GameObject> passiveSkills;        // 추가 공격 스킬
    [Header("추가 획득 스킬")]
    public List<GameObject> skills;        // 추가 공격 스킬

    // 아이템화 되어 드랍된 공격 스킬 획득시 플레이어 사용
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 장착 무기 추가
        IPlayerSkill skill = collision.GetComponent<IPlayerSkill>();

        if (skill != null)
        {
            skills.Add(collision.gameObject);
        }
    }
    private void Update()
    {
        defalutSkill.GetComponent<IPlayerSkill>().UseSkill();

        // 스킬 사용
        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].GetComponent<IPlayerSkill>().UseSkill();
            skills[i].GetComponent<IPlayerSkill>().playerVec = transform.position; // 플레이어 위치 설정
        }
    }
}
