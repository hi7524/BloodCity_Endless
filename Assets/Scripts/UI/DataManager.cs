using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerData
{
    // SettingResolution 스크립트
    public bool fullScreen;
    public int fullValue = 0;
    public int dropdownValue = 0;

    // ChaSelect 스크립트
    public int currentIndex = 0;
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
        player.currentIndex = ChaSelect.Instance.currentIndex;

        PrintData();
        SavePlayerData();
    }

    // 저장된 파일 뜯어보기 귀찮아서 이번에도 만드는 프린트함수
    public void PrintData()
    {
        Debug.Log($"지금까지 저장된 데이터는 " +
            $"\n전체화면 여부 : {player.fullScreen} \n전체화면 드롭다운 값 : {player.fullValue}" +
            $"\n해상도 드롭다운 값 : {player.dropdownValue} \n캐릭터 선택 값 : {player.currentIndex}");
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