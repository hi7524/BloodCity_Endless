using UnityEngine;
using Cinemachine;

// 플레이어를 추적하도록 설정
public class TargetCamPlayer : MonoBehaviour
{
    private GameObject player; // 플레이어 (추적 대상)
    private CinemachineVirtualCamera virtualCam;


    private void Awake()
    {
        // 컴포넌트 초기화
        virtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        // 추적 오브젝트 설정
        player = GameObject.FindWithTag("Player");
        virtualCam.Follow = player.transform;
    }
}
