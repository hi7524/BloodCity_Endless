using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 설명
public class TEST : MonoBehaviour
{
    public float[] radius; // 원의 반경
    public Color gizmoColor = Color.red; // Gizmo 색상
    public LayerMask targetLayer; // 감지할 대상 레이어

    private Collider2D[] detectedObjects;
    private Collider2D[] detectedObjects1;
    private Collider2D[] detectedObjects2;

    void Update()
    {
        DetectObjects();

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("TEST");

            foreach (var obj in detectedObjects)
            {
                obj.GetComponent<TestMonster>().DamageTest(5);
            }

            foreach (var obj in detectedObjects1)
            {
                obj.GetComponent<TestMonster>().DamageTest(5);
            }

            foreach (var obj in detectedObjects2)
            {
                obj.GetComponent<TestMonster>().DamageTest(5);
            }
        }
    }

    void DetectObjects()
    {
        // 특정 반경 내에 있는 모든 Collider2D를 감지
        detectedObjects = Physics2D.OverlapCircleAll(transform.position, radius[0], targetLayer);
        detectedObjects1 = Physics2D.OverlapCircleAll(transform.position, radius[1], targetLayer);
        detectedObjects2 = Physics2D.OverlapCircleAll(transform.position, radius[2], targetLayer);

        // 감지된 오브젝트 정보 출력
        foreach (var obj in detectedObjects)
        {
            Debug.Log("Detected object: " + obj.name);
        }
    }

    // Gizmo를 그릴 때 호출되는 메서드
    private void OnDrawGizmos()
    {
        // 지정한 색상으로 Gizmo를 설정
        Gizmos.color = gizmoColor;

        // 2D 평면 상에서의 원 그리기
        for (int i = 0; i < radius.Length; i++)
        {
            Gizmos.DrawWireSphere(transform.position, radius[i]);
        }
    }
}
