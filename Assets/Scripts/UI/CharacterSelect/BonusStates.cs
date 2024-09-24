using UnityEngine;

// 캐릭터 보너스 스탯
[CreateAssetMenu(menuName = "Scriptable/BonusStates", fileName = "BonusState")]
public class BonusStates : ScriptableObject
{
    [Header("Character")]
    public string characterName;

    [Header("States")]
    public float maxHealthBonus = 0;    // 최대 체력 보너스
    public float restorePerSecBonus = 0; // 초당 회복량 보너스
    public float defenseBonus = 0;        // 방어력 보너스
    public float speedBonus = 0;          // 이동 속도 보너스

    public float attackDamageBonus = 0; // 공격력 보너스
    public float attackRangeBonus = 0;   // 공격 범위 보너스

    public float abilityHasteBonus = 0;  // 능력 가속 보너스
    public float magnetismBonus = 0;      // 자성 보너스

    public float curseBonus = 0;          // 저주 보너스
}