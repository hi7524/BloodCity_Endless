using System.Collections;
using UnityEngine;

// 헤비 머신건 스킬
// (오토 타겟 무기) 가장 가까운 몬스터를 공격
public class HeavyMachineGun_DRAFT : MonoBehaviour
{
    public bool activeGun = false; // 무기 활성화 여부

    public GameObject bulletPrf; // 총알 프리팹
    [SerializeField] float coolTime = 1;


    private void Start()
    {
        StartCoroutine(GunCoolTime());
    }

    IEnumerator GunCoolTime()
    {
        while (activeGun)
        {
            yield return new WaitForSeconds(coolTime);
            Fire();
        }
    }

    private void Fire()
    {   
        Instantiate(bulletPrf, transform.position, transform.rotation);
    }    

}
