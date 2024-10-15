using UnityEngine;

// 헤비머신건 스킬의 총알
// 가장 가까운 적 감지 및 추적
public class HeavyMachineGunBullet : MonoBehaviour
{
    public LayerMask detectLayer; // 추적 대상 레이어 선택
    public int bulletDamage = 1;

    private float moveSpeed = 10; // 총알 속도
    private float detectRange = 5f; // 적 감지 범위
    private Transform closetTarget; // 추적 대상


    private void Start()
    {
        DetectEnemy();
    }

    private void Update()
    {
        FollowEnemy(); // 적 감지
    }

    // 적 감지 및 추적할 적 설정
    private void DetectEnemy()
    {
        // 적 감지
        MobAI[] enemies = FindObjectsOfType<MobAI>();
        closetTarget = null;
        float maxDis = Mathf.Infinity;

        // 가장 가까운 적 설정
        foreach (MobAI mob in enemies)
        {
            float targetDis = Vector2.Distance(transform.position, mob.transform.position);

            if (targetDis < maxDis)
            {
                closetTarget = mob.transform;
                maxDis = targetDis;
            }
        }
    }

    // 적 추적
    private void FollowEnemy()
    {
        // 추적 대상이 존재하지 않을 경우
        if (closetTarget == null)
        {
            Destroy(gameObject);
        }
        // 추적 대상이 존재할 경우
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, closetTarget.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌시
        if (collision.CompareTag("Enemy"))
        {
            int attackDamage = (int)(FindObjectOfType<PlayerState>().attackDamage) + bulletDamage;
            collision.GetComponent<MobAI>().Damaged(attackDamage);

            Destroy(gameObject); // 오브젝트 파괴
        }
        else
        {
            Debug.Log("충돌");
        }
    }
}
