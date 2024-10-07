using UnityEngine;

// 플레이어 코인
[CreateAssetMenu(menuName = "Scriptable/PlayerCoin", fileName = "PlayerCoin")]
public class PlayerCoinData : ScriptableObject
{
    [Header("PlayerCoin")]
    public int point = 1;
}