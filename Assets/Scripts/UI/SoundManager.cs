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

        public float BGM_value = 240;
        public float SFX_value = 240;
    }

    string GameDataFileName = "SoundData.json";

    public SoundData sound = new SoundData();

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Fill_Image")]
    public Image BGMFill;
    public Image SFXFill;

    public float BGM_volume;
    public float SFX_volume;

    public void Start()
    {
        LoadSoundData();
        LoadSetting();
        UpdateAudioMixer("BGM", BGM_volume);
        UpdateAudioMixer("SFX", SFX_volume);
    }

    public void LoadSetting()
    {
        BGM_volume = sound.BGM;
        SFX_volume = sound.SFX;

        BGMFill.GetComponent<RectTransform>().sizeDelta = new Vector2(sound.BGM_value, 15);
        SFXFill.GetComponent<RectTransform>().sizeDelta = new Vector2(sound.SFX_value, 15);
    }

    public void Btn(int num)
    {
        if (num == 0) { SetVolume(BGMFill, 0); }
        else if (num == 1) { SetVolume(BGMFill, 1); }
        else if (num == 2) { SetVolume(SFXFill, 0); }
        else if (num == 3) { SetVolume(SFXFill, 1); }
    }

    public void SetVolume(Image Fill, int type)
    {
        RectTransform width = Fill.GetComponent<RectTransform>();
        Vector2 size = width.sizeDelta; // 현재 크기 가져오기
        
        if (size.x <= 240 && type == 0) { size.x -= 24; } // <
        else if (size.x >= 0 && type == 1) { size.x += 24; } // >

        size.x = Mathf.Clamp(size.x, 0, 240); // 범위 제한
        width.sizeDelta = size; // 크기 적용

        float volume = size.x / 240 * 100; // % 변환
        if (size.x == 0) { volume = -80; }
        UpdateAudioMixer(Fill == BGMFill ? "BGM" : "SFX", volume);

        // 볼륨 값 저장
        if (Fill == BGMFill) { sound.BGM = volume; sound.BGM_value = size.x; }
        else { sound.SFX = volume; sound.SFX_value = size.x; }

        SaveSoundData();
    }

    // 사운드 설정
    private void UpdateAudioMixer(string name, float value)
    {
        Debug.Log($"현재 {name} volume 값 : {value}");
        audioMixer.SetFloat(name, Mathf.Log10(value / 100) * 20);
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