using UnityEngine;
using TMPro;
using DG.Tweening;
using static Unity.Burst.Intrinsics.X86.Avx;

public class DamageTextFloating : MonoBehaviour
{
    public float damage;
    public Color textColor = Color.red;

    private TextMeshProUGUI tmp;


    private void Start()
    {
        // 컴포넌트 초기화
        tmp = GetComponent<TextMeshProUGUI>();

        // 텍스트 이펙트
        tmp.color = textColor;
        TextEffect();
    }

    private void TextEffect()
    {
        tmp.text = damage.ToString();
        Vector3 targetVec = new Vector3(0, 0.5f, 0);
        tmp.DOFade(0, 1f);
        tmp.transform.DOMove(transform.position + targetVec, 0.5f).OnComplete(() =>{
            Destroy(transform.parent.gameObject);
        });
    }
}
