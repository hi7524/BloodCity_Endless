using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AudioManager
public class AudioManager : MonoBehaviour
{
    // 싱글톤 변수
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    [Header("Sound Clips")]
    public AudioClip coinSound;
    public AudioClip expSound;


    private AudioSource audioSource;

    void Awake()
    {
        // 싱글톤
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCoinSound()
    {
        audioSource.PlayOneShot(coinSound);
    }

    public void PlayExpSound()
    {
        audioSource.PlayOneShot(expSound);
    }
}

