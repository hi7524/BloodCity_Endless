using UnityEngine;

// 캐릭터 스탯
[CreateAssetMenu(menuName = "Scriptable/CharacterState", fileName = "CharacterState")]
public class CharacterStates : ScriptableObject
{
    [Header("Character")]
    public string characterName;

    [Header("States")]
    public float maxHealth = 0;    // 최대 체력 (+)

    public float restorePerSec = 0;  // 초당 회복량 (+)
    public float defense = 1;        // 방어력 (+)
    public float speed = 100;        // 이동 속도 (%)

    public float attackDamage = 100; // 공격력 (%)
    public float attackRange = 100;  // 공격 범위 (%)

    public float abilityHaste = 100;   // 능력 가속 (쿨감, %)
    public float magnetism = 0;      // 자성 (+)

    public float curse = 0;          // 저주 (+)
}