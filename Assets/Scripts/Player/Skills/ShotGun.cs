using Unity.VisualScripting;
using UnityEngine;

// 샷건(플레이어의 기본 공격 스킬) 구현
public class ShotGun : MonoBehaviour, IPlayerSkill
{
    public Vector3 playerVec { get; set; } // 플레이어 위치
    public float coolDown = 1;   // 스킬 쿨타임

    private float curCoolDown = 0;

    public void UseSkill()
    {
        // 쿨타임마다 스킬 사용
        curCoolDown += Time.deltaTime;

        if (curCoolDown > coolDown)
        {
            Debug.Log("shotGunSkillUsed");
            curCoolDown = 0;
        }
    }
}
