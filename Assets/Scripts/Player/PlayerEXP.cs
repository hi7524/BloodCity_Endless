using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEXP : MonoBehaviour
{
    public Image xpBar;
    public TMP_Text levelText;

    [SerializeField] float magnetStrength = 1; // 끌어당기는 힘 (클수록 플레이어에게 오는 코인 속도가 빨라짐)

    private float levelUpXp = 10; // 레벨업을 위해 필요한 경험치
    private float playerXP; // 플레이어 경험치

    private PlayerCharacterState charState; // 캐릭터 스탯
    private CircleCollider2D circleCollider2D;

    private void Awake()
    {
        // 컴포넌트 초기화
        charState = GetComponent<PlayerCharacterState>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        // 초기 설정
        circleCollider2D.radius = charState.magnetism; // 자성 범위 설정
        magnetStrength = charState.magnetism * 0.7f;   // 자석 힘 설정 (빨아들이는 빠르기)

        levelText.text = ("Lv." + GameManager.Instance.playerLevel.ToString()); // UI
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 경험치를 끌어 당김
        if (collision.CompareTag("XP"))
        {
            collision.transform.position = Vector3.Lerp(collision.transform.position, this.transform.position, magnetStrength * Time.deltaTime);
        }
    }

    // 플레이어 경험치 추가
    public void AddPlayerXP(int XP)
    {
        playerXP += XP;
    }

    // 플레이어 레벨 업
    private void LevelUp()
    {
        xpBar.fillAmount = 0; // 경험치 바 초기화
        playerXP = 0;         // 경험치 초기화
        ExpToNextLevel();     // 다음 레벨업까지 획득해야 할 경험치 계산
        GameManager.Instance.playerLevel++; // 레벨 업
        levelText.text = ("Lv." + GameManager.Instance.playerLevel.ToString()); // UI
    }

    // 레벨업을 위한 획득 경험치 계산
    private void ExpToNextLevel()
    {
        Debug.Log("다음 레벨까지 획득해야하는 경험치: " + levelUpXp);
        levelUpXp = (levelUpXp * 0.15f) + levelUpXp;
    }
    
}
