using Unity.Properties;
using UnityEngine;

// 플레이어 머신건 스킬
public class HeavyMachineGun : MonoBehaviour, IPlayerSkill
{
    public Vector3 playerVec {  get; set; } // 플레이어 위치
    public GameObject bulletPrf; // 총알 프리팹
    public float coolDown = 1;   // 스킬 쿨타임

    private float coolTime = 0;


    // 스킬 사용
    public void UseSkill()
    {
        Debug.Log("머신건!!!");
        coolTime += Time.deltaTime;

        if(coolTime > coolDown)
        {
            Instantiate(bulletPrf, playerVec, Quaternion.identity);
            coolTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
