using UnityEngine;

// �÷��̾� ����
[CreateAssetMenu(menuName = "Scriptable/PlayerCoin", fileName = "PlayerCoin")]
public class PlayerCoinData : ScriptableObject
{
    [Header("PlayerCoin")]
    public int point = 1;
}
