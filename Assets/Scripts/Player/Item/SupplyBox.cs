using UnityEngine;

// 보급 상자
public class SupplyBox : MonoBehaviour, IItem
{
    public AudioClip openBoxSound;

    private AudioSource audioSource;
    private Animator animator;

    private void Awake()
    {
        // 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

    }

    public void Use(GameObject target)
    {
        animator.SetTrigger("BoxOpen");
        audioSource.clip = openBoxSound;
        audioSource.Play();
    }

    public void OpenWindow()
    {
        UIManager.Instance.WeaponLevelUp();
    }
}
