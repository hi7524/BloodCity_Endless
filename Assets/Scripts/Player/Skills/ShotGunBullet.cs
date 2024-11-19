using UnityEngine;

// 샷건(플레이어 기본 스킬)의 총알 구현 → 날아감
public class ShotGunBullet : MonoBehaviour
{
    public float bulletLifeTime = 1.0f; // 총알이 유지될 시간
    public float minForce = 4f; // 최소 힘(속도)
    public float maxForce = 5;  // 최고 힘(속도)
    public int bulletDamage = 3; // 공격 데미지
    public GameObject damageTextPrf;

    private float force;          // 현재 총알이 가질 힘

    private Vector3 mousePos; // 마우스 위치
    private Camera mainCam;   // 마우스 위치를 계산할 카메라
    private Rigidbody2D rigid;
    private GameObject collEnemy; // 충돌 적


    private void Awake()
    {
        // 컴포넌트 초기화
        rigid = GetComponent<Rigidbody2D>();
        mainCam = Camera.main; // 카메라 설정
    }

    private void OnEnable()
    {
        Invoke("FireBullet", 0.01f);
    }

    private void FireBullet()
    {
        force = Random.Range(minForce, maxForce + 1); // 힘 랜덤 설정
        // 마우스 위치 확인 및 방향 설정
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;

        // 해당 위치로 이동
        rigid.velocity = new Vector2(direction.x + Random.Range(-1f, 1f), direction.y + Random.Range(-1f, 1f)).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        Invoke("ActiveFalse", bulletLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌시
        if (collision.CompareTag("Enemy") && collEnemy == null)
        {
            collEnemy = collision.gameObject;
            int attackDamage = (int)(FindObjectOfType<PlayerState>().attackDamage) + bulletDamage;
            collision.GetComponent<MobAI>().Damaged(attackDamage);

            // 공격 이펙트
            GameObject damageText = Instantiate(damageTextPrf);                             // 텍스트 플로팅 프리팹 생성
            damageText.GetComponentInChildren<DamageTextFloating>().damage = attackDamage;  // 텍스트로 띄울 공격력 전달

            Vector2 textVec = new Vector2(collEnemy.transform.position.x + Random.Range(-0.4f, 0.5f), collEnemy.transform.position.y);
            damageText.transform.position = textVec;

            ActiveFalse();
        }
    }

    private void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
