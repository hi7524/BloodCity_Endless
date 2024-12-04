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

    new private void Start()
    {
        base.Start();
        Destroy(gameObject, bulletLifeTime);
    }

    new private void OnTriggerEnter2D(Collider2D collision)
    {
        // 기존 충돌 행동 실행
        base.OnTriggerEnter2D(collision); 

        // 추적 대상 미정인 경우 대상 설정 (중복 방지용)
        if (!isTargetSet && collision.tag == "Enemy")
        {
            targetEnemy = collision.gameObject;
           
            StartCoroutine(TickDamage(collision.GetComponent<MobAI>()));       // 적 피격
            isTargetSet = true;                 // 추적 대상 정보 (중복 방지용)
        }
    }

    new private void Update()
    {
        base.Update();
        //Debug.Log(targetEnemy);
    }

    // 적에게 일정 간격마다 데미지
    IEnumerator TickDamage(MobAI obj)
    {
        float i = bulletLifeTime * 2;

        while (i >= 0)
        {
            obj.Damaged(attackDamage + 2);
            yield return new WaitForSeconds(damageCoolDown);
            i -= damageCoolDown;
        }
    }

    public override void Fire()
    {

    }
} 
