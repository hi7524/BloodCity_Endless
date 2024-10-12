using UnityEngine;

// 플레이어 이동·애니메이션
public class PlayerController : MonoBehaviour
{
    private PlayerState playerState; // 플레이어 정보
    private Animator animator;
    

    private void Awake()
    {
        // 컴포넌트 초기화
        animator = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();

        Debug.Log("<color=yellow>**PlayerTag 적용해주세요**</color>");
    }

    private void Update()
    {
        Move(); // 이동
        WalkAnimation(); // 이동 애니메이션
    }

    // 플레이어 이동
    private void Move()
    {
        // 플레이어가 생존한 경우에만 작동
        if (!playerState.isPlayerDead)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(x, y) * playerState.speed * Time.deltaTime;
            transform.Translate(movement);
        }
    }

    // 플레이어 이동 애니메이션
    private void WalkAnimation()
    {
        // 플레이어가 생존한 경우에만 작동
        if(!playerState.isPlayerDead)
        {   
            // 이동중엔 이동 애니메이션 재생, 멈추면 애니메이션 정지
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                animator.speed = 1.0f;
            }
            else
            {
                animator.speed = 0.0f;
            }
        }
    }
}
