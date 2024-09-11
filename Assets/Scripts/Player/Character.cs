using UnityEngine;

// 캐릭터 뼈대 제공
public class Character : MonoBehaviour
{
    // 기본 스탯
    protected float maxHealth; // 최대 체력
    protected float health; // 체력
    protected float restorePerSec;  // 초당 회복량
    protected float defense;  // 방어력
    protected float speed;  // 이동 속도 (%)
    protected float attackDamage;  // 공격력 (%)
    protected float attackRange;   // 공격 범위 (%)
    protected float abilityHaste;    // 능력 가속 (쿨감, %)
    protected float magnetism;      // 자성
    protected float curse;       // 저주

    private float storeSec = 0; // 초당 회복 계산을 위한 변수

    // 캐릭터 스탯 설정
    /*protected void SetStates(CharacterStates charState)
    {
        maxHealth = charState.maxHealth; // 최대 체력 설정
        health = maxHealth;              // 초기 체력 설정

        restorePerSec += charState.restorePerSec; // 초당 회복량 설정
        defense += charState.defense;             // 방어력 설정
        speed = 1;
        speed = speed * (charState.speed / 100);  // 이동 속도 설정

        attackDamage = 1;
        attackRange = 1;
        attackDamage = attackDamage * (charState.attackDamage / 100); // 공격력 설정
        attackRange = attackRange * (charState.attackRange / 100); // 공격 범위 설정

        abilityHaste = abilityHaste * (charState.abilityHaste / 100); // 능력 가속 (쿨감) 설정
        magnetism += charState.magnetism;                             // 자성 설정

        curse += charState.curse; // 저주 설정
    }*/

    // 초당 체력 회복
    protected void RestoreHealthPerSec()
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
            health = health - (damage - defense);
        }
    }

    // 공격

    // 자성

    // 저주

    // 사망
    public virtual void Die()
    {
        Debug.Log("플레이어 사망");
        health = 0;
    }
}