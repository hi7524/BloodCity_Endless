using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 주위로 무기가 돌도록 함
public class PlayerSkillPos : MonoBehaviour
{
    public int id;
    public int prefebId;
    public float damage;
    public float speed;
    public float count;

    private void Update()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }



    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            
        }
    }
}
