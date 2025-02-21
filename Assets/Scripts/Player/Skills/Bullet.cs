using UnityEngine;

// 총알 기본 클래스
public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    public LayerMask  detectLayer;        // 추적 대상 레이어 선택
    public int        bulletDamage = 1;   // 총알 데미지
    public float      trackingSpeed = 10; // 총알 추적 속도
    public GameObject damageTextPrf;      // 총알 피해 이펙트 (텍스트 플로팅)

    protected GameObject collEnemy;         // 충돌 적
    protected int floatingDamage;           // 적에게 입힌 데미지 (띄울 입힌 데미지)

    private float detectRange = 5f;       // 적 감지 범위
    private Transform closetTarget;       // 추적 대상
    protected int attackDamage;

    protected void Start()
    {
        attackDamage = (int)(PlayerState.Instance.attackDamage) + bulletDamage; // 공격력 계산
    }

    protected void Update()
    {
        DetectEnemy();
        FollowEnemy();
    }

    // 적 감지 및 추적할 적 설정
    protected void DetectEnemy()
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
    public virtual void FollowEnemy()
    {
        // 추적 대상이 존재하지 않을 경우
        if (closetTarget == null)
        {
            Destroy(gameObject);
        }
        // 추적 대상이 존재할 경우
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, closetTarget.transform.position, trackingSpeed * Time.deltaTime);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌시
        if (collision.CompareTag("Enemy") && collEnemy == null)
        {
            // 충돌 적 정보
            collEnemy = collision.gameObject;

            // 적 공격
            Fire();

            // 공격 이펙트 (텍스트 플로팅)
            TextFloatingEffect();
        }
    }

    // 충돌시 적 공격
    public virtual void Fire()
    {
        // 몬스터 공격
        //attackDamage = (int)(FindObjectOfType<PlayerState>().attackDamage) + bulletDamage; // 공격력 받아오기

        collEnemy.GetComponent<MobAI>().Damaged(attackDamage);                             // 몬스터 공격
        Destroy(gameObject); // 오브젝트 파괴
    }

    // 충돌 이펙트 (텍스트 플로팅)
    public virtual void TextFloatingEffect()
    {
        // 공격 이펙트
        GameObject damageText = Instantiate(damageTextPrf);                             // 텍스트 플로팅 프리팹 생성
        damageText.GetComponentInChildren<DamageTextFloating>().damage = attackDamage;  // 텍스트로 띄울 공격력 전달

        Vector2 textVec = new Vector2(collEnemy.transform.position.x + Random.Range(-0.4f, 0.5f), collEnemy.transform.position.y);
        damageText.transform.position = textVec;                   // 충돌 위치에 프리팹 생성
    }
}
