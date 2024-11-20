using System.Collections;
using UnityEngine;

// FlameBelt 스킬 (플레이어 패시브 스킬)
public class FlameBelt : MonoBehaviour
{
    public int damage = 2; // 적에게 입힐 데미지
    public float tickTime = 2; // 도트딜 간격
    public float damageRadius;
    public LayerMask damageLayer;
    public GameObject damageTextPrf;

    private void Start()
    {
        StartCoroutine(TickDamage());
    }

    // 일정 시간마다 공격
    private IEnumerator TickDamage()
    {
        while (true)
        {
            Damage();
            yield return new WaitForSeconds(tickTime);
        }
    }

    // 적 데미지
    private void Damage()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, damageRadius, damageLayer);

        foreach (Collider2D obj in hitObjects)
        {
            // MobAI 컴포넌트 가져오기
            MobAI mobAI = obj.GetComponent<MobAI>();

            if (mobAI != null) // Null 체크
            {
                // 적 데미지
                mobAI.Damaged(damage);

                // 텍스트 플로팅
                GameObject damageText = Instantiate(damageTextPrf); // 텍스트 플로팅 프리팹 생성
                damageText.GetComponentInChildren<DamageTextFloating>().damage = damage; // 텍스트로 띄울 공격력 전달
                damageText.transform.position = obj.transform.position; // 충돌 위치에 프리팹 생성
            }
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        
        Gizmos.DrawWireSphere(transform.position, damageRadius);

    }
}
