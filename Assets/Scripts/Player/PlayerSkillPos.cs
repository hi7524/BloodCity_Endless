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
        SkillRotate();
    }
    
    // 스킬 회전
    private void SkillRotate()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }

    // 스킬 배치
    public void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject bullet = Instantiate(examplePrf); 

            bullet.transform.parent = transform; 

            Vector3 rotvec = Vector3.forward * 360 * i / count; // 위치 계산
            bullet.transform.Rotate(rotvec);                    // 계산 위치에 배치
            bullet.transform.Translate(bullet.transform.up * 5f, Space.World); // 플레이어로부터 거리 띄우기
        }
    }
}
