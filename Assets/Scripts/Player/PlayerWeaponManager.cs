using System.Collections.Generic;
using UnityEngine;
using static ItemSkill;

// 스킬 배치 및 회전
public class PlayerWeaponManager : MonoBehaviour
{
    [Header("스킬 설정값")]
    public float rotateSpeed = 15; // 무기 회전 속도
    public GameObject rotateEffect; // 회전 이펙트
    public float effectSpeed = 10;

    [Header("스킬")]
    public GameObject defaultSkill; // 기본 무기 (샷건)
    public List<GameObject> playerSkills = new List<GameObject>(); // 무기 목록

    public int count { get; private set; }     // 무기 개수
    private PlayerState playerState;
    
    private void Start()
    {
        // 컴포넌트 초기화
        playerState = transform.parent.GetComponent<PlayerState>();

        count = 1 + playerSkills.Count; // 기본 소지 무기 개수                    
        playerSkills.Add(defaultSkill); // 기본 무기 추가

        Batch();                        // 초기 무기 배치
    }

    private void Update()
    {
        // 플레이어가 생존해있을 때만 작동
        if (playerState.isPlayerDead == false)
        {
            SkillRotate();
        }
    }
    
    // 스킬 회전
    private void SkillRotate()
    {
        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
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
            skill.transform.Translate(skill.transform.up * 3f, Space.World); // 플레이어로부터 거리 띄우기
        }
        
    }

    // 스킬 개수 추가 및 재배치
    public void AddSkill(GunType addGunType)
    {
        // 스킬 개수 추가
        count++;

        // 스킬 추가
        GameObject gunPrf = Resources.Load<GameObject>($"Prefabs/PlayerSkills/{addGunType}");
        playerSkills.Add(gunPrf);

        // 기존 스킬 제거 (재배치를 위함)
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // 스킬 배치
        Batch();
    }
}
