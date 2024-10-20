using UnityEngine;

// 플레이어의 체력 및 사망 상태 관리
public class PlayerHealth : MonoBehaviour
{
    private float health;   // 플레이어 체력
    private float storeSec; // 초 계산을 위한 변수

    private PlayerState playerState;
    private Animator animator;


    private void Awake()
    {
        // 컴포넌트 초기화
        playerState = GetComponent<PlayerState>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // 체력 설정
        health = playerState.maxHealth;

        Debug.Log("<color=#00FF22>[K] 플레이어 사망 테스트</color>");
    }

    private void Update()
    {
        // 체력 UI 업데이트
        UpdateHpUI();

        // 초당 체력 재생
        RestoreHealthPerSec();

        // 체력이 0일 경우 사망
        if (health <= 0)
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    // 체력 UI 업데이트
    private void UpdateHpUI()
    {
        UIManager.Instance.UpdatePlayerHealth(health, playerState.maxHealth);
    }

    // 초당 체력 회복
    private void RestoreHealthPerSec()
    {
        // 체력이 닳아있는 상태이고, 플레이어가 생존 해 있을 때
        if (health < playerState.maxHealth && !playerState.isPlayerDead)
        {
            storeSec += Time.deltaTime;

            if (storeSec > 1f)
            {
                health += playerState.restorePerSec; // 초당 회복력 만큼 회복
                storeSec = 0;
            }
        }
    }

    // 피격
    public virtual void Damaged(float damage)
    {
        if (health > 0)
        {
            damage = damage - playerState.defense;

            if(damage > 0)
                health = health - damage;
        }
    }

    // 플레이어 사망
    private void Die()
    {
        playerState.SetPlayerDead(); // 플레이어 사망 처리
        animator.speed = 1.0f;       // 애니메이션 재생
        animator.SetTrigger("Dead"); // 사망 애니메이션 재생
    }
}