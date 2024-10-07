using UnityEngine;

// 플레이어 머신건 스킬
public class HeavyMachineGun : MonoBehaviour, IPlayerSkill
{
    public Vector3 playerVec { get; set; } // 플레이어 위치
    public GameObject bulletPrf; // 총알 프리팹
    public float coolDown = 2;   // 스킬 쿨타임

    private float curCoolDown = 0;


    // 스킬 사용
    public void UseSkill()
    {
        // 쿨타임마다 스킬 사용
        curCoolDown += Time.deltaTime;

        if(curCoolDown > coolDown)
        {
            Instantiate(bulletPrf, playerVec, Quaternion.identity); // 총알 발사 (생성)
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
