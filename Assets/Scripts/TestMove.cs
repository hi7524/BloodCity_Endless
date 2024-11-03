
using UnityEngine;

// 설명
public class TestMove : MonoBehaviour
{
    private void Update()
    {
        Debug.Log("moveTest");
        transform.Translate(Vector3.right * 1f * Time.deltaTime);
    }

}
