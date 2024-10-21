using UnityEngine;

// 코인
public class Coin : MonoBehaviour, IItem
{
    public PlayerCoinData coinData;

    public void Use(GameObject target)
    {
        GameManager.Instance.AddCoin(coinData.point);
        AudioManager.Instance.PlaySound("coinSound");
        Destroy(gameObject);
    }
}
