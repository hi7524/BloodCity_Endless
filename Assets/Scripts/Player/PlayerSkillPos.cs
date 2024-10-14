using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 주위로 무기가 돌도록 함
public class PlayerSkillPos : MonoBehaviour
{
    public GameObject examplePrf;
    public float speed;
    public float count;

    private void Start()
    {
        Batch();
    }
    private void Update()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }
    
    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            GameObject bullet = Instantiate(examplePrf);

            bullet.transform.parent = transform;

            Vector3 rotvec = Vector3.forward * 360 * index / count;
            bullet.transform.Rotate(rotvec);
            bullet.transform.Translate(bullet.transform.up * 5f, Space.World);

        }
    }
}
