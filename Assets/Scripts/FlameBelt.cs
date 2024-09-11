using UnityEngine;

// FlameBelt 스킬 (플레이어 패시브 스킬)
public class FlameBelt : MonoBehaviour
{
    float currTime;

    private void Start()
    {
        Debug.Log("<color=yellow>코드 병합 후 적 구분 조건 수정할 것</color>");
    }

    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.CompareTag("Enemy"))
        {
            currTime += Time.deltaTime;
       
            if (currTime > 1)
            {
                Debug.Log("Damage");
                currTime = 0;
            }
        }
    }
}
