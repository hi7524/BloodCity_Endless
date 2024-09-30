using UnityEngine;

// 설명
public class HeavyMachineGun : MonoBehaviour, IPlayerSkill
{
    public bool isSkillActive { get; set; } // 스킨 활성화 여부
    public GameObject bulletPrf; // 총알 프리팹
    public float coolDown = 1;   // 스킬 쿨타임

    private float coolTime = 0;

    private void Start()
    {
        isSkillActive = true;
    }

    // 스킬 사용
    public void UseSkill()
    {
        
        // 스킬 사용 활성화 조건일때만
        if (isSkillActive)
        {
            coolTime += Time.deltaTime;

            if(coolTime > coolDown)
            {
                Debug.Log("Skill_HeavyMachineGun");
                Debug.Log("Skill_HeavyMachineGun");
                Instantiate(bulletPrf, transform.position, transform.rotation);
                coolTime = 0;
            }
        }
    }
}
