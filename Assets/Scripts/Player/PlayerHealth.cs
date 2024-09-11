using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 플레이어의 체력 관리
public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Image healthBarImg;

    private float health;        // 플레이어 체력
    private float maxHealth;     // 최대 체력
    private float restorePerSec; // 초당 회복할 체력
    private float storeSec;      // 초 계산을 위한 변수

    private PlayerCharacterState charState; // 캐릭터 스탯


    private void Awake()
    {
        // 컴포넌트 초기화
        charState = GetComponent<PlayerCharacterState>();
    }

    private void Start()
    {
        SetHealthStates();
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
    }

    public void SetHealthStates()
    {
        health = charState.health;
        maxHealth = charState.maxHealth;
        restorePerSec = charState.restorePerSec;
    }

    // 체력 UI 업데이트
    private void UpdateHpUI()
    {
        healthText.text = $"{health.ToString()} / {maxHealth.ToString()}";
        healthBarImg.fillAmount = Mathf.Lerp(healthBarImg.fillAmount, health / maxHealth, Time.deltaTime * 10);
    }

    // 초당 체력 회복
    private void RestoreHealthPerSec()
    {
        // 체력이 닳아있는 상태이고, 초당 회복력이 0이 아닐 때 초당 체력 회복
        if (health < maxHealth && restorePerSec > 0)
        {
            storeSec += Time.deltaTime;

            if (storeSec > 1f)
            {
                Debug.Log("체력 회복");
                health += restorePerSec; // 초당 회복력 만큼 회복
                storeSec = 0;
            }
        }
    }

    // 피격
    public virtual void Damaged(float damage)
    {
        if (health > 0)
        {
            Debug.Log("방어력 적용 (데미지 - 방어력)");
            health = health - (damage - charState.defense);
        }
    }

    // 플레이어 사망
    private void Die()
    {
        health = 0;
        Debug.Log("플레이어 사망");
    }
}