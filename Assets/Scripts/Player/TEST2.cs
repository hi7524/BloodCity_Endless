using UnityEngine;

// 설명
public class TEST2 : MonoBehaviour
{
    public float maxDamage = 15; // 최대 데미지 (중심에 가까울수록 더 강함)
    public float detectionRadius = 5f; // 스킬의 감지 반경
    public LayerMask targetLayer; // 타겟 레이어 (피해를 입힐 대상)
    public GameObject explosionParticle;
    public float testNum;


    private void Start()
    {
        testNum = detectionRadius / 3;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ApplyDamage();
        }
    }

    void ApplyDamage()
    {
        // 감지 반경 내의 모든 오브젝트를 가져옴
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, detectionRadius, targetLayer);

        // 폭발 이펙트
        explosionParticle.SetActive(true);

        foreach (Collider2D obj in hitObjects)
        {

            // 중심점과 오브젝트 사이의 거리 계산
            float distance = Vector2.Distance(transform.position, obj.transform.position);


            if (distance <= testNum)
            {
                obj.GetComponent<TestMonster>().DamageTest(25);
            }
            else if (distance <= testNum*2)
            {
                obj.GetComponent<TestMonster>().DamageTest(15);
            }
            else
            {
                obj.GetComponent<TestMonster>().DamageTest(10);
            }

        }
    }

    // 감지 반경 시각화
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, testNum);
        Gizmos.DrawWireSphere(transform.position, testNum*2);
        Gizmos.DrawWireSphere(transform.position, testNum*3);
        //Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
