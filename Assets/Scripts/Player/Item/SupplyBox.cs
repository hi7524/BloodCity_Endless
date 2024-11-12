using UnityEngine;

// 보급 상자
public class SupplyBox : MonoBehaviour, IItem
{
    private Animator animator;


    private void Awake()
    {
        // 컴포넌트 초기화
        animator = GetComponent<Animator>();
    }

    public void Use(GameObject target)
    {
        animator.SetTrigger("BoxOpen");
        UIManager.Instance.WeaponLevelUp();
    }
}
