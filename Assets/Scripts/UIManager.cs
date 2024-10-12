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
    public GameObject pauseWindow; // 옵션 창
    public GameObject levelUpWindow; // 레벨업시 나타날 스텟 업그레이드 창

    [Header("플레이어 체력")]
    public Image healthBarImg;
    public TMP_Text healthText;


    void Awake()
    {
        // 싱글톤
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        // 초기 설정
        pauseWindow.SetActive(false); // 옵션 창 비활성화
        levelUpWindow.SetActive(false); // 레벨업 창 비활성화
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
        healthText.text = $"{health.ToString()} / {maxHealth.ToString()}";
        healthBarImg.fillAmount = Mathf.Lerp(healthBarImg.fillAmount, health / maxHealth, Time.deltaTime * 10);
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
