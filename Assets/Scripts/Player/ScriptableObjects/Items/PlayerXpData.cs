using UnityEngine;

// �÷��̾� ����ġ
[CreateAssetMenu(menuName = "Scriptable/PlayerXP", fileName = "PlayerXP")]
public class PlayerXpData : ScriptableObject
{
    [Header("PlayerXP")]
    public int point = 1;
}
