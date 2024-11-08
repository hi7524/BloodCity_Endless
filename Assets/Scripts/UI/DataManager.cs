using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class PlayerData
{
    [Header("SettingResolution")]
    public bool fullScreen;
    public int fullValue = 0;
    public int dropdownValue = 0;

    [Header("StartScene")]
    public int currentIndex = 0;
    public int stageIndex;

    [Header("Enhance")]
    public int coins;
    public float[] characterStats = new float[9]; // 캐릭터 스탯 배열
    public int[] enhanceLevels = new int[9]; // 각 EnhanceState의 업그레이드 레벨
    public bool[] isUpgraded = new bool[9]; // 각 EnhanceState의 업그레이드 여부

    [Header("Skill")]
    public int level = 1;
    public int engauge;
    public bool[] skillON = new bool[3]; // 각 SkillStates의 업그레이드 여부
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private string GameDataFileName = "PlayerData.json";

    public PlayerData player = new PlayerData();

    public void LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            player = JsonUtility.FromJson<PlayerData>(FromJsonData);
            Debug.Log("<color=lime>[SUCCESS]</color> 플레이어 데이터 불러오기 완료");
        }
    }

    public void SavePlayerData()
    {
        string ToJsonData = JsonUtility.ToJson(player);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Data();

        Debug.Log("<color=yellow>[CHEAT]</color> 쉬프트 + P - 데이터 출력");
        Debug.Log("<color=yellow>[CHEAT]</color> 쉬프트 + R - 플레이어 스탯 데이터 리셋");
        Debug.Log("<color=yellow>[CHEAT]</color> 쉬프트 + G - 코인 증가 + 1000");
    }

    private void Update()
    {
        // 쉬프트 + P 데이터 출력
        if (Input.GetKeyDown(KeyCode.P) && Input.GetKey(KeyCode.LeftShift))
        {
            PrintData();
        }
        // 쉬프트 + R 플레이어 스탯 데이터 리셋
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift))
        {
            UpgradeManager.Instance.ResetStates();
        }
        // 쉬프트 + G 코인 증가
        if (Input.GetKeyDown(KeyCode.G) && Input.GetKey(KeyCode.LeftShift))
        {
            player.coins += 1000;
            UpgradeManager.Instance.currentCoins = player.coins;
            UpgradeManager.Instance.UpdateCoinsText();
        }
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
        // 모든 저장시기에 같이 저장되어야 할 거 아니면 스크립트에서 직접 저장하는 형식으로 추후 바꿀 예정
        player.fullScreen = SettingResolution.Instance.fullScreen;
        player.fullValue = SettingResolution.Instance.full.value;
        player.dropdownValue = SettingResolution.Instance.dropdown.value;
        player.currentIndex = ChaSelect.Instance.currentIndex;

        SavePlayerData();
    }

    

    // 저장된 값
    public void PrintData()
    {
        Debug.Log($"<color=cyan>[INFO]</color> 지금까지 저장된 데이터 " +
            $"\n전체화면 여부 : {player.fullScreen} \n전체화면 드롭다운 값 : {player.fullValue}" +
            $"\n해상도 드롭다운 값 : {player.dropdownValue} \n캐릭터 선택 값 : {player.currentIndex}" +
            $"\n코인 : {player.coins}" +
            "\n캐릭터 스탯: " + string.Join(", ", player.characterStats) +
            "\nEnhanceLevels: " + string.Join(", ", player.enhanceLevels) +
            "\nIsUpgraded: " + string.Join(", ", player.isUpgraded));
    }

    // 데이터 초기화
    public void DataDelete()
    {
        if (File.Exists(Application.persistentDataPath + "/" + GameDataFileName))
        {
            File.Delete(Application.persistentDataPath + "/" + GameDataFileName);
            File.Delete(Application.persistentDataPath + "/SoundData.json");

            SceneManager.LoadScene("StartScene");
            UpgradeManager.Instance.ResetStates();
        }
        else
        {
            ToolTipManager.Instance.TipOn("<color=orange>[WARNING]</color> 저장된 데이터가 없습니다.");
        }
        ToolTipManager.Instance.TipOn("<color=lime>[SUCCESS]</color> 데이터가 삭제되었습니다.");
    }
}
