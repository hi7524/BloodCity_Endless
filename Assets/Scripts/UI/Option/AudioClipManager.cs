using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : MonoBehaviour
{
    public static AudioClipManager Instance { get; private set; }

    public AudioClip audioClick;

    AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
    }
}