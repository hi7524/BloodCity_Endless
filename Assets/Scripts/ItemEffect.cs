using DG.Tweening;
using UnityEngine;

// 아이템 이펙트
public class ItemEffect : MonoBehaviour
{
    private void Start()
    {
        // 아이템 이펙트(위아래로 흔들림)
        transform.DOBlendableLocalMoveBy(new Vector3(0, 0.5f, 0), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }
}
