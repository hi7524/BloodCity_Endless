using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class SoundManager : MonoBehaviour
{
    // 싱글톤
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public class SoundData
    {
        public float BGM = 1.0f;
        public float SFX = 1.0f;
    }

    string GameDataFileName = "SoundData.json";

    public SoundData sound = new SoundData();


    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("UI Sliders")]
    public Slider BGMSlider;
    public Slider SFXSlider;

    public void Start()
    {
        LoadSoundData();
        BGMSlider.value = sound.BGM;
        SFXSlider.value = sound.SFX;
        UpdateAudioMixer();
    }

    // 0이 배경음 1이 효과음
    public void SetVolume(int type)
    {
        if (type == 0)
        {
            sound.BGM = BGMSlider.value;
        }
        else if (type == 1)
        {
            sound.SFX = SFXSlider.value;
        }

        UpdateAudioMixer();
        SaveSoundData();
    }
    
    // 사운드 설정
    private void UpdateAudioMixer()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(sound.BGM) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(sound.SFX) * 20);
    }

    // 사운드 불러오기
    public void LoadSoundData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            sound = JsonUtility.FromJson<SoundData>(FromJsonData);
            Debug.Log("사운드 데이터 불러오기 완료");
        }
    }

    // 사운드 저장하기
    public void SaveSoundData()
    {
        string ToJsonData = JsonUtility.ToJson(sound);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("사운드 데이터 저장 완료");
    }
}