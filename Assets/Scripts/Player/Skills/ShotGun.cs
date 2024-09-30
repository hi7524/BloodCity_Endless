using Unity.VisualScripting;
using UnityEngine;

// 샷건(플레이어의 기본 공격 스킬) 구현
public class ShotGun : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bulletPrf;
    public Transform bulletTrans;
    public float coolDown;
    private float curCoolDown;

    private void Start()
    {
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
            Instantiate(bulletPrf, bulletTrans.position, Quaternion.identity);
            curCoolDown = 0;
        }
        
    }
}
