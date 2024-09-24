using UnityEngine;

// 헤비머신건 스킬의 총알
public class HeavyMachineGunBullet : MonoBehaviour
{
    public LayerMask detectLayer; // 레이어 선택
    private float moveSpeed = 10; // 총알 속도
    private float detectRange = 5f; // 적 감지 범위


    private void Update()
    {
        DetectEnemy(); // 적 감지
    }

    // 적 감지 및 추적
    private void DetectEnemy()
    {
        // 가장 가까운 몬스터 감지
        MobAI[] enemies = FindObjectsOfType<MobAI>();
        Transform closetTarget = null;
        float maxDis = Mathf.Infinity;

        foreach (MobAI mob in enemies)
        {
            float targetDis = Vector2.Distance(transform.position, mob.transform.position);

            if (targetDis < maxDis)
            {
                closetTarget = mob.transform;
                maxDis = targetDis;
            }
        }

        // 총알 발사(추적)
        transform.position = Vector2.MoveTowards(transform.position, closetTarget.transform.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌시
        if (collision.CompareTag("Enemy"))
        {
            int attackDamage = (int)(FindObjectOfType<PlayerState>().attackDamage);
            print("몬스터에게 가한 피해 : " + attackDamage);
            collision.GetComponent<MobAI>().Damaged(attackDamage);

            Destroy(gameObject); // 오브젝트 파괴
        }
    }
}
