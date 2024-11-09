using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CharacterState", fileName = "CharacterState")]
public class CharacterStates : ScriptableObject
{
    [Header("Character")]
    public string characterName;

    [Header("States")]
    public float maxHealth = 100;    // 최대 체력 (+)
    public float restorePerSec = 0;  // 초당 회복량 (+)
    public float defense = 1;        // 방어력 (+)
    public float speed = 100;        // 이동 속도 (%)

    public float attackDamage = 100; // 공격력 (%)
    public float attackRange = 100;  // 공격 범위 (%)

    public float abilityHaste = 0;   // 능력 가속 (쿨감, %)
    public float magnetism = 0;      // 자성 (+)

    public float curse = 0;          // 저주 (+)

    public void Initialize()
    {
        maxHealth = 100; // 기본값
        restorePerSec = 0;
        defense = 1;
        speed = 100;
        attackDamage = 100;
        attackRange = 100;
        abilityHaste = 0;
        magnetism = 0;
        curse = 0;

        DataManager.Instance.player.characterStats[0] = maxHealth;
        DataManager.Instance.player.characterStats[1] = restorePerSec;
        DataManager.Instance.player.characterStats[2] = defense;
        DataManager.Instance.player.characterStats[3] = speed;
        DataManager.Instance.player.characterStats[4] = attackDamage;
        DataManager.Instance.player.characterStats[5] = attackRange;
        DataManager.Instance.player.characterStats[6] = abilityHaste;
        DataManager.Instance.player.characterStats[7] = magnetism;
        DataManager.Instance.player.characterStats[8] = curse;
    }

    public float GetStat(string statName)
    {
        switch (statName)
        {
            case "maxHealth": return maxHealth;
            case "restorePerSec": return restorePerSec;
            case "defense": return defense;
            case "speed": return speed;
            case "attackDamage": return attackDamage;
            case "attackRange": return attackRange;
            case "abilityHaste": return abilityHaste;
            case "magnetism": return magnetism;
            case "curse": return curse;
            default: throw new System.ArgumentException("Invalid stat name: " + statName);
        }
    }

    public void SetStat(string statName, float value)
    {
        switch (statName)
        {
            case "maxHealth": maxHealth = value; break;
            case "restorePerSec": restorePerSec = value; break;
            case "defense": defense = value; break;
            case "speed": speed = value; break;
            case "attackDamage": attackDamage = value; break;
            case "attackRange": attackRange = value; break;
            case "abilityHaste": abilityHaste = value; break;
            case "magnetism": magnetism = value; break;
            case "curse": curse = value; break;
            default: throw new System.ArgumentException("Invalid stat name: " + statName);
        }
    }

    [Header("Detail")]
    [TextArea(2, 8)] public string characterDetail;
}