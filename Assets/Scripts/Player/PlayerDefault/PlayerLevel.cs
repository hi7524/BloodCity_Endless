using UnityEngine;

// 플레이어의 경험치 및 레벨업 관리
public class PlayerLevel : MonoBehaviour
{
    private float playerLevel = 0; // 플레이어 레벨
    private float playerXP;        // 현재 플레이어가 획득한 경험치
    private float levelUpXp;        // 레벨업을 위해 필요한 경험치


    private void Start()
    {
        // 경험치 초기 설정
        levelUpXp = 10;
    }

    private void Update()
    {
        // 경험치바 업데이트
        UIManager.Instance.xpBar.fillAmount = Mathf.Lerp(UIManager.Instance.xpBar.fillAmount, playerXP / levelUpXp, Time.deltaTime * 10);

        // 레벨업
        if (UIManager.Instance.xpBar.fillAmount >= 0.99)
        {
            LevelUp();
        }
    }

    // 경험치 추가
    public void AddExp(int addExp)
    {
        playerXP += addExp;
    }

    // 플레이어 레벨 업
    private void LevelUp()
    {
        // UI
        playerLevel++;
        UIManager.Instance.xpBar.fillAmount = 0;  // 경험치 바 초기화
        playerXP -= levelUpXp; // 경험치 초기화

        // 데이터
        ExpToNextLevel();      // 다음 레벨업까지 획득해야 할 경험치 계산
        GameManager.Instance.PlayerLevelUp(); // 레벨 업
        UIManager.Instance.levelText.text = "Lv. " + playerLevel.ToString(); // 레벨 나타냄
        PlayerLevelUPManager.Instance.GenerateLevelUpCards();
        UIManager.Instance.ToggleWindow(UIManager.Instance.levelUpWindow); // 레벨업 창 활성화
    }

    // 레벨업을 위한 획득 경험치 계산
    private void ExpToNextLevel()
    {
        levelUpXp = (levelUpXp * 0.15f) + levelUpXp;
    }
}