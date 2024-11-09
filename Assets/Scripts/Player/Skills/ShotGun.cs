using DG.Tweening;
using System.Collections;
using UnityEngine;

// 샷건(플레이어의 기본 공격 스킬) 구현
public class ShotGun : MonoBehaviour
{
    [Header("ShotGun")]
    public GameObject bulletPrf;
    public Transform bulletTrans;
    public float coolDown;
    public int bulletCount = 6; // 발사 총알 개수

    [Header("Effect")]
    public AudioClip fireSound;
    public ParticleSystem fireParticle;

    private float curCoolDown;
   
    private Camera mainCam;
    private Vector3 mousePos;
    private AudioSource audioSource;


    private void Start()
    {
        // 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        // 초기 설정
        mainCam = Camera.main;
    }

    private void Update()
    {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 rotation = mousePos - transform.position;

            float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotationZ);


            // 쿨타임마다 스킬 사용
            curCoolDown += Time.deltaTime;

            if (curCoolDown > coolDown)
            {
                StartCoroutine(Fire());
                curCoolDown = 0;
                fireParticle.Play(); // 파티클
            }
        
    }
    
    IEnumerator Fire()
    {
        // 흔들림 효과
        transform.DOShakePosition(0.2f, 0.2f, 1, 1);

        // 이펙트
        audioSource.clip = fireSound;
        audioSource.Play();

        // 총알 생성
        for (int i = 0; i < bulletCount; i++)
        {
            Instantiate(bulletPrf, bulletTrans.position, Quaternion.identity);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
