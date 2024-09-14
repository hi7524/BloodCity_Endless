using UnityEngine;

// 플레이어의 이동 및 기본 기능 구현
// 이동, 아이템 획득
public class PlayerController : MonoBehaviour
{
    [Header("Item")]
    public LayerMask detectLayer; // 레이어 선택

    private float detectRange; // 아이템 감지 범위
    private float magnetStrength  = 3.5f; // 아이템 당기는 힘(속도)
     
    private PlayerState charState; // 캐릭터 스탯

    private void Awake()
    {
        // 컴포넌트 초기화
        charState = GetComponent<PlayerState>();
    }

    private void Start()
    {
        detectRange = charState.magnetism; // 자석 범위 설정

        if (charState.magnetism > 3)
        {
            magnetStrength = charState.magnetism * 0.9f;   // 자석 힘 설정 (빨아들이는 빠르기)
        }
    }

    private void Update()
    {
        Move();
        DetectItem();
    }

    // 플레이어 이동
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(x, y) * charState.speed * Time.deltaTime;
        transform.Translate(movement);
    }

    // 주변 아이템 감지
    private void DetectItem()
    {
        // 아이템 감지
        var itemObj = Physics2D.OverlapCircleAll(transform.position, detectRange, detectLayer);

        // 아이템 끌어당기기
        if (itemObj != null)
        {
            for (int i = 0; i < itemObj.Length; i++)
            {
                itemObj[i].transform.position = Vector3.Lerp(itemObj[i].transform.position, this.transform.position, magnetStrength * Time.deltaTime);
            }
        }
    }

    // 아이템 감지 범위 나타냄
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 아이템 사용
        IItem item = collision.GetComponent<IItem>();

        if (item != null)
        {
            item.Use(gameObject);
        }
    }
}