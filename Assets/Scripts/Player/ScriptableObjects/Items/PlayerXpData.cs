using UnityEngine;

// 플레이어 경험치
[CreateAssetMenu(menuName = "Scriptable/PlayerXP", fileName = "PlayerXP")]
public class PlayerXpData : ScriptableObject
{
    [Header("PlayerXP")]
    public int point = 1;
}
