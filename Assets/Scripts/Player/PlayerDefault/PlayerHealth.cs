using UnityEngine;

// 플레이어의 체력 및 사망 상태 관리
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;            // 플레이어 체력
    [SerializeField] private float maxHealth = 100;   // 최대 체력
    [SerializeField] private float restorePerSec = 1; // 초당 회복할 체력
    [SerializeField] private float storeSec;          // 초 계산을 위한 변수
    [SerializeField] private float defense = 1;


    private void Start()
    {
        Debug.Log("<color=cyan>**스탯 연결 필요** (체력 관련 정보, 방어력)</color>");
        health = maxHealth; // 체력 초기화
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

    // 체력 UI 업데이트
    private void UpdateHpUI()
    {
        UIManager.Instance.UpdatePlayerHealth(health, maxHealth);
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
            damage = damage - defense;

            if(damage > 0)
                health = health - damage;
        }
    }

    // 플레이어 사망
    private void Die()
    {
        health = 0;
        Debug.Log("플레이어 사망");
    }
}