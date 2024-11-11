using UnityEngine;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    public ScrollRect scrollRect; // 스크롤 뷰
    public float moveAmount = 360f; // 이동 거리
    public Button leftButton;
    public Button rightButton;

    private void Start()
    {
        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);
    }

    private void MoveLeft()
    {
        // 현재 스크롤 위치
        float newPosition = scrollRect.content.anchoredPosition.x + moveAmount;

        // 스크롤 범위 제한 (왼쪽 끝)
        if (newPosition > 0) newPosition = 0;

        scrollRect.content.anchoredPosition = new Vector2(newPosition, scrollRect.content.anchoredPosition.y);
    }

    private void MoveRight()
    {
        // 현재 스크롤 위치
        float newPosition = scrollRect.content.anchoredPosition.x - moveAmount;

        // 콘텐츠 너비
        float contentWidth = scrollRect.content.sizeDelta.x;
        float viewportWidth = scrollRect.viewport.rect.width;

        // 스크롤 범위 제한 (오른쪽 끝)
        float maxPosition = -contentWidth + viewportWidth;

        if (newPosition < maxPosition) newPosition = maxPosition;

        scrollRect.content.anchoredPosition = new Vector2(newPosition, scrollRect.content.anchoredPosition.y);
    }
}
