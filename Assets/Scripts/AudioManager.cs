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
    public AudioClip playerHitSound;
    public AudioClip addSkillSound;
        

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

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "coinSound":
                audioSource.PlayOneShot(coinSound);
                break;
            case "expSound":
                audioSource.PlayOneShot(expSound);
                break;
            case "playerHitSound":
                audioSource.PlayOneShot(playerHitSound);
                break;
            case "addSkillSound":
                audioSource.PlayOneShot(addSkillSound);
                break;
            default:
                Debug.Log("재생할 효과음이 없습니다.");
                break;
        }
    }

}

