using UnityEngine;

// 플레이어 캐릭터의 스탯 정보를 적용
public class PlayerState : MonoBehaviour
{ 
    public bool isPlayerDead { get; private set; } // 플레이어 사망 여부
    public float maxHealth { get; private set; } // 최대 체력
    public float restorePerSec { get; private set; }  // 초당 회복량
    public float defense { get; private set; }  // 방어력
    public float speed { get; private set; }  // 이동 속도 (%)
    public float attackDamage { get; private set; }  // 공격력 (%)
    public float attackRange { get; private set; }   // 공격 범위 (%)
    public float abilityHaste { get; private set; }    // 능력 가속 (쿨감, %)
    public float magnetism { get; private set; }      // 자성
    public float curse { get; private set; }       // 저주


    private void Awake()
    {
        Debug.Log("<color=cyan>**예진이 스크립트랑 연결**</color>");
        SetStateDRAFT();
    }

    private void SetStateDRAFT()
    {
        isPlayerDead = false;  // 플레이어 사망 여부
        maxHealth = 100; // 최대 체력
        restorePerSec = 1;   // 초당 회복량
        defense = 1;  // 방어력
        speed = 4f;  // 이동 속도 (%)
        attackDamage = 1;   // 공격력 (%)
        attackRange = 0;  // 공격 범위 (%)
        abilityHaste = 0;  // 능력 가속 (쿨감, %)
        magnetism = 1;  // 자성
        curse = 0; // 저주
    }

    public void SetPlayerDead()
    {
        Debug.Log("플레이어 사망");
        isPlayerDead = true;
    }


/*    // 초기 스탯 설정
    private void SetStates(CharacterStates charState)
    {
        maxHealth = defaultState.maxHealth + charState.maxHealth; // 최대 체력 설정
        health = maxHealth;              // 초기 체력 설정

        restorePerSec = defaultState.restorePerSec + charState.restorePerSec; // 초당 회복량 설정
        defense = defaultState.restorePerSec + charState.defense;             // 방어력 설정
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


}