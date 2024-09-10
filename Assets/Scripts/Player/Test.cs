using UnityEngine;

// 플레이어 피격 테스트 스크립트
public class Test : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float testDamage = 10;

    private void Start()
    {
        Debug.Log("[G] 플레이어 피격 테스트");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("데미지 주기");
            playerHealth.Damaged(testDamage);
        }
    }
}