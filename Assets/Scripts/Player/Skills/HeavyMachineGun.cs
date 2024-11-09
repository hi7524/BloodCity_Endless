using DG.Tweening;
using UnityEngine;

// 플레이어 머신건 스킬
public class HeavyMachineGun : MonoBehaviour
{
    [Header("Skill")]
    public Transform firePos;    // 총구 위치
    public GameObject bulletPrf; // 총알 프리팹
    public float coolDown = 2;   // 스킬 쿨타임
    [Header("Effect")]
    public float shakeDuration = 0.1f;  // 총기 흔들림 효과 지속 시간
    public float shakeIntens = 0.1f;    // 총기 흔들림 효과 강도
    public AudioClip fireSound;         // 총 발사 효과음
    public ParticleSystem fireParticle; // 총 발사 파티클

    private float curCoolDown = 0;
    private Transform closetTarget; // 추적 대상
    private AudioSource audioSource;

    private void Awake()
    {
        // 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        DetectEnemy();
        FollowEnemy();
    }

    // 적 감지 및 추적할 적 설정
    private void DetectEnemy()
    {
        // 적 감지
        MobAI[] enemies = FindObjectsOfType<MobAI>();
        closetTarget = null;
        float maxDis = Mathf.Infinity;

        // 가장 가까운 적 설정
        foreach (MobAI mob in enemies)
        {
            float targetDis = Vector2.Distance(transform.position, mob.transform.position);

            if (targetDis < maxDis)
            {
                closetTarget = mob.transform;
                maxDis = targetDis;
            }
        }
    }

    // 적 추적
    private void FollowEnemy()
    {
        if (UIManager.Instance.gameEndWindow.activeSelf == false)
        {
            // 추적 대상이 존재하지 않을 경우
            if (closetTarget == null)
            {

            }
            // 추적 대상이 존재할 경우
            else
            {
                // 적의 방향으로 회전 (적을 바라봄)
                Vector3 rotation = closetTarget.position - transform.position;
                float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, rotationZ), 5 * Time.deltaTime);

                // 총알 발사
                Fire();
            }
        }
    }

    // 스킬 사용
    public void Fire()
    {
        // 쿨타임마다 스킬 사용
        curCoolDown += Time.deltaTime;

        // Fire
        if (curCoolDown > coolDown)
        {
            // 흔들림 효과
            transform.DOShakePosition(shakeDuration, shakeIntens, 1, 1);

            // 효과음
            audioSource.clip = fireSound;
            audioSource.Play();

            // 이펙트
            fireParticle.Play();

            // 총알 생성
            Instantiate(bulletPrf, firePos.position, Quaternion.identity);
            curCoolDown = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어의 스킬 획득 처리
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
