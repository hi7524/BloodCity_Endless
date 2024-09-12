using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerData
{
    // SettingResolution ��ũ��Ʈ
    public bool fullScreen;
    public int fullValue = 0;
    public int dropdownValue = 0;

    // �� �� ��ũ��Ʈ���� ������ ��
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
            print("�÷��̾� ������ �ҷ����� �Ϸ�");
        }
    }

    public void SavePlayerData()
    {
        string ToJsonData = JsonUtility.ToJson(player);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        print("�÷��̾� ������ ���� �Ϸ�");
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
        print("���� ������ ���� ��...");
        
        player.fullScreen = SettingResolution.Instance.fullScreen;
        player.fullValue = SettingResolution.Instance.full.value;
        player.dropdownValue = SettingResolution.Instance.dropdown.value;

        SavePlayerData();
    }

    // ������ �ʱ�ȭ
    public void DataDelete()
    {
        if (File.Exists(Application.persistentDataPath + "/" + GameDataFileName))
        {
            Debug.Log("����� �����Ͱ� �ֽ��ϴ�.");
            File.Delete(Application.persistentDataPath + "/" + GameDataFileName);
            File.Delete(Application.persistentDataPath + "/SoundData.json");

            SceneManager.LoadScene("StartScene");
        }
        else
        {
            Debug.Log("���� �� �����Ͱ� �����ϴ�.");
        }
        Debug.Log("�����Ͱ� �����Ǿ����ϴ�.");
    }
}