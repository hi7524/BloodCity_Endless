using UnityEngine;
using UnityEngine.EventSystems;

// ** 테스트를 위한 임시 스크립트 ** 
// 레벨업 버튼 클릭시 게임 재진행
public class StateLevelUp_DRAFT : MonoBehaviour, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("스탯 레벨 업");
        UIManager.Instance.ToggleWindow(UIManager.Instance.levelUpWindow); // 레벨업 창 토글
    }
}
