using DG.Tweening;
using UnityEngine;

// 스킬 아이템화 → 획득시 플레이어 스킬에 해당 스킬 추가
public class ItemSkill : MonoBehaviour
{
    public int skillID = 0; // 플레이어에게 추가할 스킬 ID
    // HeavyMachineGun = 1
    // RocketLauncher = 2

    private void Start()
    {
        // 아이템 이펙트
        transform.DOBlendableLocalMoveBy(new Vector3(0, 0.5f, 0), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 감지
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<PlayerSkillManager>().AddSkill(skillID); // 플레이어 스킬 추가
            AudioManager.Instance.PlaySound("addSkillSound");                         // 효과음 재생
            Destroy(gameObject);                                                      // 오브젝트 파괴
        }
    }
}
