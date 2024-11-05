using System.Collections;
using UnityEngine;

// 프리즈마 건 총알 구현
public class PlasmaGunBullet : Bullet
{
    public float damageCoolDown = 0.5f;  // 틱 데미지 간격
    public float bulletLifeTime = 5f;
    public ParticleSystem bulletParticle;

    GameObject targetEnemy; // 추적 대상
    private bool isTargetSet = false; // 중복 실행 방지 위한 변수

    private void Start()
    {
        base.Start();
        Destroy(gameObject, bulletLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 기존 충돌 행동 실행
        base.OnTriggerEnter2D(collision); 

        // 추적 대상 미정인 경우 대상 설정 (중복 방지용)
        if (!isTargetSet)
        {
            targetEnemy = collision.gameObject; 
            StartCoroutine(TickDamage());       // 적 피격
            StartCoroutine(CheckTargetEnemy()); // 적 사망 여부 확인
            isTargetSet = true;                 // 추적 대상 정보 (중복 방지용)
        }
    }

    private void Update()
    {
        base.Update();
        Debug.Log(targetEnemy);
    }

    // 적 사망 여부 확인
    IEnumerator CheckTargetEnemy()
    {
        while (true)
        {
            // 적 사망시 총알 파괴 
            if (targetEnemy.activeSelf == false)
            {
                StopCoroutine(TickDamage());
                Destroy(gameObject);
                yield break;
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    // 적에게 일정 간격마다 데미지
    IEnumerator TickDamage()
    {
        while (true)
        {
            targetEnemy.GetComponent<MobAI>().Damaged(attackDamage);
            TextFloatingEffect();
            yield return new WaitForSeconds(damageCoolDown);
        }
    }

    public override void Fire()
    {

    }
} 
