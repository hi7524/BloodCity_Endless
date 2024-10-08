using UnityEngine;

// 플레이어의 이동 및 이동 애니메이션 적용
public class PlayerController : MonoBehaviour
{     
    private PlayerState charState; // 캐릭터 스탯

    private Animator animator;

    private void Awake()
    {
        // 컴포넌트 초기화
        charState = GetComponent<PlayerState>();
        animator = GetComponent<Animator>();
    }   

    private void Update()
    {
        Move();
        WalkAnimation();
    }

    // 플레이어 이동
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(x, y) * charState.speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void WalkAnimation()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical")!= 0)
        {
            animator.speed = 1.0f;
        }
        else
        {
            animator.speed = 0.0f;
        }
    }
}