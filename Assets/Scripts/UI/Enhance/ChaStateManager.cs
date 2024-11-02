using System.Diagnostics;

[System.Serializable]
public class ChaStateManager
{
    public CharacterStates playerStates; // 여기에 Player
    public CharacterStates bonusStates; // 여기에 캐릭터별 오브젝트

    public float[] stateList = new float[9];

    // 기본 스탯 + 영구 강화
    float maxHealth = 100;    // 최대 체력
    float restorePerSec = 0; // 초당 회복량
    float defense = 1;        // 방어력
    float speed = 100;        // 이동 속도 (%)

    float attackDamage = 100; // 공격력 (%)
    float attackRange = 100;   // 공격 범위 (%)

    float abilityHaste = 0;  // 능력 가속 (쿨감, %)
    float magnetism = 0;       // 자성

    float curse = 0;           // 저주

    // 캐릭터 보너스 스탯
    float maxHealthBonus = 0;    // 최대 체력 보너스
    float restorePerSecBonus = 0; // 초당 회복량 보너스
    float defenseBonus = 0;        // 방어력 보너스
    float speedBonus = 0;          // 이동 속도 보너스

    float attackDamageBonus = 0; // 공격력 보너스
    float attackRangeBonus = 0;   // 공격 범위 보너스

    float abilityHasteBonus = 0;  // 능력 가속 보너스
    float magnetismBonus = 0;      // 자성 보너스

    float curseBonus = 0;          // 저주 보너스

    // 강화한 데이터 받아오기
    public void InitializeStats()
    {
        maxHealth = playerStates.maxHealth;
        restorePerSec = playerStates.restorePerSec;
        defense = playerStates.defense;
        speed = playerStates.speed;
        attackDamage = playerStates.attackDamage;
        attackRange = playerStates.attackRange;
        abilityHaste = playerStates.abilityHaste;
        magnetism = playerStates.magnetism;
        curse = playerStates.curse;

        maxHealthBonus = bonusStates.maxHealth;
        restorePerSecBonus = bonusStates.restorePerSec;
        defenseBonus = bonusStates.defense;
        speedBonus = bonusStates.speed;
        attackDamageBonus = bonusStates.attackDamage;
        attackRangeBonus = bonusStates.attackRange;
        abilityHasteBonus = bonusStates.abilityHaste;
        magnetismBonus = bonusStates.magnetism;
        curseBonus = bonusStates.curse;
    }

    // 스탯을 문자열로 반환
    public void GetStats()
    {
        stateList[0] = maxHealth + maxHealthBonus;
        stateList[1] = restorePerSec + restorePerSecBonus;
        stateList[2] = defense + defenseBonus;
        stateList[3] = speed + speedBonus;
        stateList[4] = attackDamage + attackDamageBonus;
        stateList[5] = attackRange + attackRangeBonus;
        stateList[6] = abilityHaste + abilityHasteBonus;
        stateList[7] = magnetism + magnetismBonus;
        stateList[8] = curse + curseBonus;
    }
}