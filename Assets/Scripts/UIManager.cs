using UnityEngine;
using UnityEngine.UI;
using TMPro;

// UI Manager
public class UIManager : MonoBehaviour
{
    // 싱글톤 선언
    private static UIManager instance;
    public static UIManager Instance
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

    [Header("UI")]
    public GameObject mainWindow;   // 메인UI 창
    public GameObject pauseWindow;   // 옵션 창
    public GameObject gameEndWindow; // 게임 완료시 나타날 창
    public GameObject gameWinWindow; // 게임 승리시 나타날 창
    public GameObject levelUpWindow; // 레벨업시 나타날 스텟 업그레이드 창
    public GameObject weaponLevelUp;
    public Text coinText;  // 인게임 코인 텍스트

    [Header("플레이어 체력")]
    public Image healthBarImg;  // 체력바
    public Text healthText; // 체력 텍스트

    [Header("플레이어 경험치")]
    public Image xpBar;        // 경험치 바
    public Text levelText; // 레벨 텍스트


    void Awake()
    {
        // 싱글톤
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // 초기 설정
        pauseWindow.SetActive(false);   // 옵션 창 비활성화
        levelUpWindow.SetActive(false); // 레벨업 창 비활성화
        weaponLevelUp.SetActive(false); // 무기 강화 창 비활성화
        gameEndWindow.SetActive(false);
        gameWinWindow.SetActive(false); 
        coinText.text = $"{GameManager.Instance.coin}   G"; // 기존 소지 코인 나타내기
    }

    private void Update()
    {
        // ESC키를 누를시 일시정지
        if (Input.GetButtonDown("ESC"))
        {
            // 효과음?
            Pause();
        }
    }

    // 플레이어 체력 정보 업데이트
    public void UpdatePlayerHealth(float health, float maxHealth)
    {
        if (health > 0)
        {
            healthText.text = $"{health:F0} / {maxHealth:F0}";
            healthBarImg.fillAmount = Mathf.Lerp(healthBarImg.fillAmount, health / maxHealth, Time.deltaTime * 10);
        }
        else
        {
            healthText.text = "0 /" + maxHealth.ToString();
            healthBarImg.fillAmount = 0;
        }
    }

    // 플레이어 코인 정보 업데이트
    public void UpdateCoin(int coin)
    {
        coinText.text = $"{coin:#,0}   G";
    }

    // 창 활성화 및 비활성화
    public void ToggleWindow(GameObject window)
    {
        // 창 끄기
        if (window.activeSelf)
        {
            window.SetActive(false);
            Time.timeScale = 1.0f;
        }
        // 창 켜기
        else
        {
            window.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    // 게임 결과 창 활성화
    public void ActiveEndWindow()
    {
        gameEndWindow.SetActive(true);
    }

    // 무기 강화 창 토글
    public void WeaponLevelUp()
    {
        ToggleWindow(weaponLevelUp);
    }

    // 게임 일시정지
    public void Pause()
    {
        // 창 끄기
        if (pauseWindow.activeSelf)
        {
            // 효과음?
            pauseWindow.SetActive(false);
            Time.timeScale = 1.0f;
        }
        // 창 켜기
        else
        {
            // 버튼 효과음
            pauseWindow.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    // 게임 종료
    public void Exit()
    {
        Application.Quit();
    }
}
