using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageTextFloating : MonoBehaviour
{
    public float damage;
    private TextMeshProUGUI tmp;


    private void Start()
    {
        // 컴포넌트 초기화
        tmp = GetComponent<TextMeshProUGUI>();

        // 텍스트 이펙트
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
