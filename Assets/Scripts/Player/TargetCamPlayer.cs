using UnityEngine;
using Cinemachine;

// 플레이어를 추적하도록 설정
public class TargetCamPlayer : MonoBehaviour
{
    public static TargetCamPlayer Instance;

    private GameObject player; // 플레이어 (추적 대상)
    private CinemachineVirtualCamera virtualCam;


    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        // 컴포넌트 초기화
        virtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void CameraOn()
    {
        // 추적 오브젝트 설정
        player = GameObject.FindWithTag("Player");
        virtualCam.Follow = player.transform;
    }
}
