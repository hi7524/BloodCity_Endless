using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerData
{
    // SettingResolution 스크립트
    public bool fullScreen;
    public int fullValue = 0;
    public int dropdownValue = 0;

    // 그 외 스크립트에서 저장할 거
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    string GameDataFileName = "PlayerData.json";

    public PlayerData player = new PlayerData();

    public void LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            player = JsonUtility.FromJson<PlayerData>(FromJsonData);
            print("플레이어 데이터 불러오기 완료");
        }
    }

    public void SavePlayerData()
    {
        string ToJsonData = JsonUtility.ToJson(player);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        print("플레이어 데이터 저장 완료");
    }

    public void Awake()
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

        Data();
    }

    public void Data()
    {
        if (!File.Exists(Application.persistentDataPath + "/" + GameDataFileName))
        {
            Save();
        }
        else
        {
            LoadPlayerData();
        }

        SettingResolution.Instance.ScreenData();
    }

    public void Save()
    {
        print("게임 데이터 저장 중...");
        
        player.fullScreen = SettingResolution.Instance.fullScreen;
        player.fullValue = SettingResolution.Instance.full.value;
        player.dropdownValue = SettingResolution.Instance.dropdown.value;

        SavePlayerData();
    }

    // 데이터 초기화
    public void DataDelete()
    {
        if (File.Exists(Application.persistentDataPath + "/" + GameDataFileName))
        {
            Debug.Log("저장된 데이터가 있습니다.");
            File.Delete(Application.persistentDataPath + "/" + GameDataFileName);
            File.Delete(Application.persistentDataPath + "/SoundData.json");

            SceneManager.LoadScene("StartScene");
        }
        else
        {
            Debug.Log("저장 된 데이터가 없습니다.");
        }
        Debug.Log("데이터가 삭제되었습니다.");
    }
}