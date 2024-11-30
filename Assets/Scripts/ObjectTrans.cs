using System.Collections;
using UnityEngine;

// 오브젝트 투명화
public class ObjectTrans : MonoBehaviour
{
    SpriteRenderer parentSpriteRenderer;

    private void Awake()
    {
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            parentSpriteRenderer = parentTransform.GetComponent<SpriteRenderer>(); // SpriteRenderer 가져오기
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeToAlpha());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeIn());
        }
    }

    // 오브젝트를 투명하게 설정함
    IEnumerator FadeToAlpha()
    {
        float elapsedTime = 0f;
        Color color = parentSpriteRenderer.color;

        while (parentSpriteRenderer.color.a > 0.5f)
        {
            color.a = Mathf.Lerp(1f, 0.5f, elapsedTime / 0.5f);
            parentSpriteRenderer.color = color;

            elapsedTime += 0.01f;
            yield return null; // 다음 프레임까지 대기
        }
    }

    // 오브젝트 투명도를 원래 상태로 돌려놓음
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = parentSpriteRenderer.color;

        while (parentSpriteRenderer.color.a < 1.0f)
        {
            color.a = Mathf.Lerp(0.5f, 1f, elapsedTime / 0.5f);
            parentSpriteRenderer.color = color;

            elapsedTime += 0.01f;
            yield return null; // 다음 프레임까지 대기
        }
    }
}
