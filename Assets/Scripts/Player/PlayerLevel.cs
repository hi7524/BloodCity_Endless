using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 플레이어의 경험치 및 레벨업 관리
public class PlayerLevel : MonoBehaviour
{
    public Image xpBar;        // 경험치 바
    public TMP_Text levelText; // 레벨 텍스트

    private float playerLevel = 0; // 플레이어 레벨
    private float playerXP;        // 현재 플레이어가 획득한 경험치
    public float levelUpXp;        // 레벨업을 위해 필요한 경험치


    private void Start()
    {
        // 경험치 초기 설정
        levelUpXp = 10;
    }

    private void Update()
    {
        // 경험치바 업데이트
        xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, playerXP / levelUpXp, Time.deltaTime * 10);

        // 레벨업
        if (xpBar.fillAmount >= 0.99)
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
        xpBar.fillAmount = 0;  // 경험치 바 초기화
        playerXP -= levelUpXp; // 경험치 초기화

        // 데이터
        ExpToNextLevel();      // 다음 레벨업까지 획득해야 할 경험치 계산
        GameManager.Instance.PlayerLevelUp(); // 레벨 업
        levelText.text = ("Lv." + GameManager.Instance.playerLevel.ToString()); // UI
        UIManager.Instance.ToggleWindow(UIManager.Instance.levelUpWindow); // 레벨업 창 활성화
    }

    // 레벨업을 위한 획득 경험치 계산
    private void ExpToNextLevel()
    {
        levelUpXp = (levelUpXp * 0.15f) + levelUpXp;
    }
}