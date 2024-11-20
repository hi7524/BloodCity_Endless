using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

// 로켓 런쳐 총알 스크립트
public class RocketLauncherBullet : Bullet
{
    [Header("RocketLauncher")]
    public float damageRadius = 3f;      // 스킬 데미지 반경
    public GameObject explosionParticle; // 폭발 이펙트
    public AudioClip  explosionSound;    // 폭발 사운드
    public Collider2D[] hitObjects;

    private float distanceThreshold;     // 데미지 기준점
    private Vector3 textVec;             // 텍스트를 띄울 위치
    private bool isExplosioned = false;  // 폭발 여부 (폭발 중복 방지 변수)
    private AudioSource audioSource;

    private void Start()
    {
        // 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        distanceThreshold = damageRadius / 3; // 데미지 차별을 주기 위해 영향 범위를 3개로 나눔
    }

    public override void Fire()
    {
        // 폭발이 일어나지 않았을 경우에만 작동하도록 함
        if (!isExplosioned)
        {
            // 폭발 여부 변경
            isExplosioned = true;

            // 데미지 반경 내의 오브젝트 감지
            hitObjects = Physics2D.OverlapCircleAll(transform.position, damageRadius, detectLayer);

            // 폭발 이펙트
            explosionParticle.SetActive(true); // 파티클 실행
            audioSource.clip = explosionSound; // 효과음 재생
            audioSource.Play();

            // 오브젝트 비활성화
            Invoke("SetActiveFalse", 1f);

            // 범위 내 감지된 오브젝트 들에 영향
            foreach (Collider2D obj in hitObjects)
            {
                // 중심점과 오브젝트 사이의 거리 계산
                float distance = Vector2.Distance(transform.position, obj.transform.position);

                // 거리에 따른 데미지 적용
                if (distance <= distanceThreshold)
                {
                    int attackDamage = (int)(FindObjectOfType<PlayerState>().attackDamage) + 25; // 공격력 받아오기
                    obj.GetComponent<MobAI>().Damaged(attackDamage);

                    GameObject damageText = Instantiate(damageTextPrf);                             // 텍스트 플로팅 프리팹 생성
                    damageText.GetComponentInChildren<DamageTextFloating>().damage = attackDamage;  // 텍스트로 띄울 공격력 전달
                    damageText.GetComponentInChildren<DamageTextFloating>().textColor = new Color(1f, 0.8235f, 0f);
                    damageText.transform.position = obj.transform.position;                         // 충돌 위치에 프리팹 생성
                }
                else if (distance <= distanceThreshold * 2)
                {
                    int attackDamage = (int)(FindObjectOfType<PlayerState>().attackDamage) + 15; // 공격력 받아오기
                    obj.GetComponent<MobAI>().Damaged(attackDamage);

                    GameObject damageText = Instantiate(damageTextPrf);                             // 텍스트 플로팅 프리팹 생성
                    damageText.GetComponentInChildren<DamageTextFloating>().damage = attackDamage;  // 텍스트로 띄울 공격력 전달
                    damageText.GetComponentInChildren<DamageTextFloating>().textColor = new Color(1f, 0.6588f, 0f);
                    damageText.transform.position = obj.transform.position;
                }
                else
                {
                    int attackDamage = (int)(FindObjectOfType<PlayerState>().attackDamage) + 10; // 공격력 받아오기

                    // MobAI 컴포넌트 가져오기
                    MobAI mobAI = obj.GetComponent<MobAI>();

                    if (mobAI != null) // Null 체크
                    {
                        obj.GetComponent<MobAI>().Damaged(attackDamage);

                        GameObject damageText = Instantiate(damageTextPrf);                             // 텍스트 플로팅 프리팹 생성
                        damageText.GetComponentInChildren<DamageTextFloating>().damage = attackDamage;  // 텍스트로 띄울 공격력 전달
                        damageText.transform.position = obj.transform.position;
                    }
                }
            }

            trackingSpeed = 0;
        }
  
    }

    // 데미지 반경 시각화
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < 3; i++)
        {
            Gizmos.DrawWireSphere(transform.position, distanceThreshold * i);
        }

        Gizmos.DrawWireSphere(transform.position, damageRadius);
        
    }

    public override void TextFloatingEffect()
    {
        // 나중에 수정하깅
    }

    // 오브젝트 비활성화
    private void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
     

    
}
