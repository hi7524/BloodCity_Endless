using System.Collections;
using UnityEngine;

// 수류탄
public class Grenade : MonoBehaviour
{
    [Header("수류탄")]
    public Vector3 targetVec; // 목표 이동 위치
    public LayerMask damageLayer; // 데미지를 입힐 대상 레이어
    public int damage;            // 폭발시 적에게 입힐 데미지
    public float damageRadius;    // 폭발 범위
    public float speed = 18f; // 폭탄 이동 속도

    [Header("이펙트")]
    public GameObject fireEffect; // 폭발 효과
                                  // 효과음
    public GameObject damageTextPrf; // 텍스트 플로팅

    private bool isFired = false; // 중복 방지용

    private void Update()
    {
        // 목표 위치로 이동
        transform.position = Vector2.MoveTowards(transform.position, targetVec, speed * Time.deltaTime);

        // 이동 및 폭발
        if (transform.position == targetVec)
        {
            Fire();
        }
    }

    // 수류탄 폭발
    virtual public void Fire()
    {
        if (!isFired)
        {
            isFired = true;
            fireEffect.SetActive(true);
            // 데미지 반경 내의 오브젝트 감지
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, damageRadius, damageLayer);

            // 감지된 적 데미지
            foreach (Collider2D obj in hitEnemies)
            {
                // 적 데미지
                obj.GetComponent<MobAI>().Damaged(damage);
                ActivateSkill(obj);

                // 텍스트 플로팅
                GameObject damageText = Instantiate(damageTextPrf);                       // 텍스트 플로팅 프리팹 생성
                damageText.GetComponentInChildren<DamageTextFloating>().damage = damage;  // 텍스트로 띄울 공격력 전달
                damageText.transform.position = obj.transform.position;
            }

            StartCoroutine(ActiveFalseGrenades());
        }
    }

    // 수류탄 스킬 고유의 스킬 실행
    virtual public void ActivateSkill(Collider2D obj)
    {
        
    }

    // 일정 시간 뒤에 오브젝트 비활성화 및 초기화
    IEnumerator ActiveFalseGrenades()
    {
        yield return new WaitForSeconds(0.5f);
        isFired = false;
        fireEffect.SetActive(false);
        gameObject.SetActive(false);
        transform.position = transform.parent.position;
    }

    // 데미지 범위 시각화
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
