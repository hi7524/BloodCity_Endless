using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : MonoBehaviour
{
    // 재생되는 오디오 클립을 모두 관리하는 스크립트

    public static AudioClipManager Instance { get; private set; }

    // BGM

    // 시작화면
    // 씬마다 배경을 다르게 갈건지 창마다 다르게 갈건지


    // SFX

    // UI
    // 버튼 클릭시
    public AudioClip audioClick;
    
    // Player
     
    // Enemy
    

    AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // 오디오 소스 불러오기
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "Click":
                audioSource.clip = audioClick;
                break;
        }
        audioSource.Play();

        // 동시에 플레이 되어야 한다면 오디오 소스를 여러개 불러서 위치를 다르게 하는게 좋을듯
        // BGMSource / SFXSource 도 UI 소리 몬스터 소리 플레이어 소리 다 들려야 하니까 
    }
}