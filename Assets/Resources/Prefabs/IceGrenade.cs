using System.Collections;
using UnityEngine;

// 수류탄
public class IceGrenade : MonoBehaviour
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Fire();
        }

         transform.position = Vector2.MoveTowards(transform.position, targetVec, speed * Time.deltaTime);
        

        if (transform.position == targetVec)
        {
            Fire();
        }
    }

    // 수류탄 폭발
    public void Fire()
    {
        fireEffect.SetActive(true);

        // 데미지 반경 내의 오브젝트 감지
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, damageRadius, damageLayer);

        // 감지된 적 데미지
        foreach (Collider2D obj in hitEnemies)
        {
            // 적 데미지
            obj.GetComponent<MobAI>().Damaged(damage);
            obj.GetComponent<MobAI>().obj.speed = 100;
            obj.GetComponent<SpriteRenderer>().color = new Color(0.38f, 0.77f, 1f, 1f);

            // 텍스트 플로팅
            GameObject damageText = Instantiate(damageTextPrf);                       // 텍스트 플로팅 프리팹 생성
            damageText.GetComponentInChildren<DamageTextFloating>().damage = damage;  // 텍스트로 띄울 공격력 전달
            damageText.transform.position = obj.transform.position;
        }

        StartCoroutine(ThrowGrenades());
    }

    IEnumerator ThrowGrenades()
    {
        yield return new WaitForSeconds(0.5f);

        fireEffect.SetActive(false);
        gameObject.SetActive(false);
        transform.position = transform.parent.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
