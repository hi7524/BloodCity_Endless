using UnityEngine;

// 설명
public class SupplyBox : MonoBehaviour, IItem
{
    public void Use(GameObject target)
    {
        UIManager.Instance.WeaponLevelUp();
    }
}
