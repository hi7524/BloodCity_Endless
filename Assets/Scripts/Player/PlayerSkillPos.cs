using System.Collections.Generic;
using UnityEngine;

// 플레이어 주위로 무기가 돌도록 함
public class PlayerSkillPos : MonoBehaviour
{
    [Header("스킬 설정값")]
    public float speed = 15; // 무기 회전 속도
    [Header("스킬")]
    public GameObject defaultSkill; // 기본 무기 (샷건)
    public List<GameObject> playerSkills = new List<GameObject>(); // 무기 목록

    private float count;     // 무기 개수

    
    private void Start()
    {
        count = 1;                      // 시작시 기본 무기 소지 시작 고정을 위한 개수 초기화
        playerSkills.Add(defaultSkill); // 기본 무기 추가

        Batch();                        // 초기 무기 배치
    }

    private void Update()
    {
        SkillRotate();
    }
    
    // 스킬 회전
    private void SkillRotate()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }

    // 스킬 배치
    public void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            // 생성 및 설정
            GameObject skill = Instantiate(playerSkills[i]);
            skill.transform.parent = transform;

            // 위치값 초기화
            skill.transform.localPosition = Vector3.zero; // 위치 초기화
            skill.transform.localRotation = Quaternion.identity; // 회전 초기화

            // 위치값 설정 및 배치
            Vector3 rotvec = Vector3.forward * 360 * i / count; // 위치 계산
            skill.transform.Rotate(rotvec);                    // 계산 위치에 배치
            skill.transform.Translate(skill.transform.up * 5f, Space.World); // 플레이어로부터 거리 띄우기
        }
        
    }

    // 스킬 개수 추가 및 재배치
    public void AddSkill()
    {
        // 기존 스킬 제거 (재배치를 위함)
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // 스킬 개수 추가
        count++;

        // 스킬 배치
        Batch();
    }

    // Trrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 스킬 충돌시 충돌 스킬 추가
        if (collision.CompareTag("PlayerSkill"))
        {
            playerSkills.Add(collision.gameObject); // 충돌한 아이템 추가

            AddSkill();
        }   
    }
}
