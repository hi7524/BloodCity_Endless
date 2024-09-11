using UnityEditor;
using UnityEngine;

// 플레이어의 이동 구현
public class PlayerController : MonoBehaviour
{
    private PlayerCharacterState charState; // 캐릭터 스탯


    private void Awake()
    {
        // 컴포넌트 초기화
        charState = GetComponent<PlayerCharacterState>();
    }

    private void Update()
    {
        Move();
    }

    // 플레이어 이동
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(x, y) * charState.speed * Time.deltaTime;
        transform.Translate(movement);
    }
}