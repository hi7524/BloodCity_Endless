using UnityEngine;

// 경험치
public class Exp : MonoBehaviour, IItem
{
    public PlayerXpData xpData;

    public void Use(GameObject target)
    {
        target.GetComponent<PlayerState>().AddExp(xpData.point);
        Destroy(gameObject);
    }
}
