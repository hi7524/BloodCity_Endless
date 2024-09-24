[System.Serializable]
public class ChaStateManager
{
    public CharacterStates playerStates;
    public BonusStates bonusStates;

    // 기본 스탯
    float maxHealth = 100;    // 최대 체력
    float restorePerSec = 0; // 초당 회복량
    float defense = 1;        // 방어력
    float speed = 100;        // 이동 속도 (%)

    float attackDamage = 100; // 공격력 (%)
    float attackRange = 100;   // 공격 범위 (%)

    float abilityHaste = 0;  // 능력 가속 (쿨감, %)
    float magnetism = 0;       // 자성
    float curse = 0;           // 저주

    // 보너스 스탯
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

        maxHealthBonus = bonusStates.maxHealthBonus;
        restorePerSecBonus = bonusStates.restorePerSecBonus;
        defenseBonus = bonusStates.defenseBonus;
        speedBonus = bonusStates.speedBonus;
        attackDamageBonus = bonusStates.attackDamageBonus;
        attackRangeBonus = bonusStates.attackRangeBonus;
        abilityHasteBonus = bonusStates.abilityHasteBonus;
        magnetismBonus = bonusStates.magnetismBonus;
        curseBonus = bonusStates.curseBonus;
    }

    // 스탯을 문자열로 반환
    public string GetStats()
    {
        string stats = $"최대 체력 : {maxHealth}";
        if (maxHealthBonus != 0)
        {
            stats += $" <color=red>+ {maxHealthBonus}</color>";
        }
        stats += $"\n회복 : {restorePerSec}";
        if (restorePerSecBonus != 0)
        {
            stats += $" <color=red>+ {restorePerSecBonus}</color>";
        }
        stats += $"\n방어력 : {defense}";
        if (defenseBonus != 0)
        {
            stats += $" <color=red>+ {defenseBonus}</color>";
        }
        stats += $"\n이동 속도 : {speed}";
        if (speedBonus != 0)
        {
            stats += $" <color=red>+ {speedBonus}</color>";
        }
        stats += $"\n\n피해량 : {attackDamage}";
        if (attackDamageBonus != 0)
        {
            stats += $" <color=red>+ {attackDamageBonus}</color>";
        }
        stats += $"\n공격 범위: {attackRange}";
        if (attackRangeBonus != 0)
        {
            stats += $" <color=red>+ {attackRangeBonus}</color>";
        }
        stats += $"\n\n쿨타임: {abilityHaste}";
        if (abilityHasteBonus != 0)
        {
            stats += $" <color=red>+ {abilityHasteBonus}</color>";
        }
        stats += $"\n자석: {magnetism}";
        if (magnetismBonus != 0)
        {
            stats += $" <color=red>+ {magnetismBonus}</color>";
        }
        stats += $"\n\n저주: {curse}";
        if (curseBonus != 0)
        {
            stats += $" <color=red>+ {curseBonus}</color>";
        }

        return stats;
    }
}