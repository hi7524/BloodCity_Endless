using UnityEngine;
using DG.Tweening;

// 플레이어의 체력 및 사망 상태 관리
public class PlayerHealth : MonoBehaviour
{
    private float health;   // 플레이어 체력
    private float storeSec; // 초 계산을 위한 변수

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public bool isDead = false;


    private void Awake()
    {
        // 컴포넌트 초기화
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        // 체력 설정
        health = PlayerState.Instance.maxHealth;
    }

    private void Update()
    {
        // 체력 UI 업데이트
        UpdateHpUI();

        // 초당 체력 재생
        RestoreHealthPerSec();

        // 체력이 0일 경우 사망
        if (health <= 0 && !isDead)
        {
            Debug.Log("Dead");
            isDead = true;  // 중복 호출 방지용
            Die();
        }

        if (Input.GetKeyDown(KeyCode.K) && Input.GetKey(KeyCode.LeftShift))
        {
            Die();
        }

    }

    // 체력 UI 업데이트
    private void UpdateHpUI()
    {
        UIManager.Instance.UpdatePlayerHealth(health, PlayerState.Instance.maxHealth);
    }

    // 초당 체력 회복
    private void RestoreHealthPerSec()
    {
        // 체력이 닳아있는 상태이고, 플레이어가 생존 해 있을 때
        if (health < PlayerState.Instance.maxHealth && !PlayerState.Instance.isPlayerDead)
        {
            storeSec += Time.deltaTime;

            if (storeSec > 1f)
            {
                health += PlayerState.Instance.restorePerSec; // 초당 회복력 만큼 회복
                storeSec = 0;
            }
        }
    }

    // 피격
    public virtual void Damaged(float damage)
    {
        if (health > 0)
        {
            // 방어력 적용 피격 데미지 계산
            damage = damage - PlayerState.Instance.defense;

            // 계산한 데미지가 0 초과일 경우, 데미지 입힘
            if (damage > 0) { health -= damage; }
            if (damage <= 0) { health--; }
            
            AudioManager.Instance.PlaySound("playerHitSound");

            // 피격 이펙트 (색상 변경)
            spriteRenderer.DOColor(new Color32(0xFF, 0x73, 0x73, 0xFF), 0.1f).OnComplete(() =>
            {
                spriteRenderer.DOColor(Color.white, 0.1f);
            });
        }
    }

    // 플레이어 사망
    private void Die()
    {
        PlayerState.Instance.SetPlayerDead(); // 플레이어 사망 처리
        animator.speed = 1.0f;       // 애니메이션 재생
        animator.SetTrigger("Dead"); // 사망 애니메이션 재생
    }

    public void DeadWindow()
    {
        UIManager.Instance.ActiveEndWindow();
    }
}