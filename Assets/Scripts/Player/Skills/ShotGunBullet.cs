using UnityEngine;

// 샷건(플레이어 기본 스킬)의 총알 구현 → 날아감
public class ShotGunBullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rigid;
    public float force = 5;

    private void Start()
    {
        mainCam = Camera.main;
        rigid = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rigid.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌시
        if (collision.CompareTag("Enemy"))
        {
            int attackDamage = (int)(FindObjectOfType<PlayerState>().attackDamage);
            collision.GetComponent<MobAI>().Damaged(attackDamage);

            Destroy(gameObject); // 오브젝트 파괴
        }
    }
}
