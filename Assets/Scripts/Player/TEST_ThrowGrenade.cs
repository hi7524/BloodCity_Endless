using System.Collections;
using UnityEngine;

// 수류탄 투척
public class TEST_ThrowGrenade : MonoBehaviour
{
    public int grenadeCount;      // 폭발할 수류탄 개수
    public GameObject grenadePrf; // 수류탄 프리팹
    public float radius;          // 폭발 위치 (radius가 클수록 멀리서 터짐)

    public GameObject[] grenades;

    private void Start()
    {
        // 생성할 수류탄 개수만큼 배열 크기 초기화
        grenades = new GameObject[grenadeCount];

        // 수류탄 생성
        for (int i = 0; i < grenadeCount; i++)
        {
            GameObject grenade = Instantiate(grenadePrf);

            grenade.transform.position = transform.position;
            grenade.transform.parent = transform;
            grenade.SetActive(false);

            grenades[i] = grenade;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(ThrowGrenades());
        }
    }

    // 수류탄 목표 위치 계산
    IEnumerator ThrowGrenades()
    {
        for (int i = 0; i < grenadeCount; i++)
        {
            float angle = 360f * i / grenadeCount;

            float radian = angle * Mathf.Deg2Rad;

            float x = transform.position.x + Mathf.Cos(radian) * radius;
            float y = transform.position.y + Mathf.Sin(radian) * radius;

            Vector2 position = new Vector2(x, y);

            grenades[i].SetActive(true);
            grenades[i].GetComponent<IceGrenade>().targetVec = position;

            yield return new WaitForSeconds(0.05f);
        }
    }


}
