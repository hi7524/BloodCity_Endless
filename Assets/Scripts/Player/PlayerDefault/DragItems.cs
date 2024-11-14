using UnityEngine;

// 아이템 끌어당김 구현
public class DragItems : MonoBehaviour
{
    public LayerMask detectLayer; // 레이어 선택

    private float detectRange; // 아이템 감지 범위
    private float magnetStrength = 3.5f; // 아이템 당기는 힘(속도)

    private void Magnet()
    {
        detectRange = PlayerState.Instance.magnetism * 0.3f; // 자석 범위 설정

        if (detectRange > 3)
        {
            magnetStrength = detectRange * 0.1f;   // 자석 힘 설정 (빨아들이는 빠르기)
        }
    } 

    private void Update()
    {
        Magnet();
        DetectItem();
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
