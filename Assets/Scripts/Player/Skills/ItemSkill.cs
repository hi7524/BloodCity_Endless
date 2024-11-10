using UnityEngine;

// 스킬 아이템화 → 획득시 플레이어 스킬에 해당 스킬 추가
public class ItemSkill : MonoBehaviour
{
    public enum GunType 
    { 
        HeavyMachineGun,
        PlasmaGun,
        ShaftGun,
        RailGun,
        RocketLauncher
    }
    public GunType selectedWeapon;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 감지
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<PlayerWeaponManager>().AddSkill(selectedWeapon); // 플레이어 스킬 추가
            AudioManager.Instance.PlaySound("addSkillSound");                                // 효과음 재생
            Destroy(gameObject);                                                             // 오브젝트 파괴
        }
    }
}
