using UnityEngine;

// ĳ���� ����
[CreateAssetMenu(menuName = "Scriptable/CharacterState", fileName = "CharacterState")]
public class CharacterStates : ScriptableObject
{
    [Header("Character")]
    public string characterName;

    [Header("States")]
    public float maxHealth = 0;    // �ִ� ü�� (+)

    public float restorePerSec = 0;  // �ʴ� ȸ���� (+)
    public float defense = 1;        // ���� (+)
    public float speed = 100;        // �̵� �ӵ� (%)

    public float attackDamage = 100; // ���ݷ� (%)
    public float attackRange = 100;  // ���� ���� (%)

    public float abilityHaste = 100;   // �ɷ� ���� (��, %)
    public float magnetism = 0;      // �ڼ� (+)

    public float curse = 0;          // ���� (+)
}