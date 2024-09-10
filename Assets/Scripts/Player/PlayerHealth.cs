using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 플레이어의 체력 관리
public class PlayerHealth : Character
{
    public TMP_Text healthText;
    public Image healthBarImg;

    private PlayerCharacterState playerCharState; // 캐릭터 스탯


    private void Awake()
    {
        // 컴포넌트 초기화
        playerCharState = GetComponent<PlayerCharacterState>();
        SetStates(playerCharState.charState);
    }

    private void Update()
    {
        // 체력 UI 업데이트
        healthText.text = $"{maxHealth.ToString()} / {health.ToString()}";
        healthBarImg.fillAmount = Mathf.Lerp(healthBarImg.fillAmount, health / maxHealth, Time.deltaTime * 10);

        // 체력이 0일 경우 사망
        if (health <= 0)
        {
            health = 0;
            Die();
        }

        // 초당 체력 재생
        RestoreHealthPerSec(); 
    }
}