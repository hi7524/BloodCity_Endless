using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : MonoBehaviour
{
    // ����Ǵ� ����� Ŭ���� ��� �����ϴ� ��ũ��Ʈ

    public static AudioClipManager Instance { get; private set; }

    // BGM

    // ����ȭ��
    // ������ ����� �ٸ��� ������ â���� �ٸ��� ������


    // SFX

    // UI
    // ��ư Ŭ����
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

        // ����� �ҽ� �ҷ�����
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

        // ���ÿ� �÷��� �Ǿ�� �Ѵٸ� ����� �ҽ��� ������ �ҷ��� ��ġ�� �ٸ��� �ϴ°� ������
        // BGMSource / SFXSource �� UI �Ҹ� ���� �Ҹ� �÷��̾� �Ҹ� �� ����� �ϴϱ� 
    }
}
