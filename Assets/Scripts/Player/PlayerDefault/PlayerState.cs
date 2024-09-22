using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 캐릭터의 스탯 정보를 적용
public class PlayerState : MonoBehaviour
{ 
    [Header("캐릭터 스탯")]
    public CharacterStates charState;

    [Header("레벨 및 경험치")]
    public Image xpBar;        // 경험치 바
    public TMP_Text levelText; // 레벨 텍스트

    // 기본 스탯
    public float maxHealth { get; private set; } // 최대 체력
    public float health { get; private set; } // 체력
    public float restorePerSec { get; private set; }  // 초당 회복량
    public float defense { get; private set; }  // 방어력
    public float speed { get; private set; }  // 이동 속도 (%)
    public float attackDamage { get; private set; }  // 공격력 (%)
    public float attackRange { get; private set; }   // 공격 범위 (%)
    public float abilityHaste { get; private set; }    // 능력 가속 (쿨감, %)
    public float magnetism { get; private set; }      // 자성
    public float curse { get; private set; }       // 저주
    public float levelUpXp { get; private set; }  // 레벨업을 위해 필요한 경험치
    public float playerXP { get; private set; }  // 플레이어 경험치

    private float playerLevel = 0;
    private float magnetSpeed = 1; // 끌어당기는 속도
    private float storeSec = 0; // 초당 회복 계산을 위한 변수

    private void Awake()
    {
        // 플레이어 스탯 셋업
        SetStates(charState);
        Debug.Log("선택 캐릭터: " + charState.name);
    }

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

    // 초기 스탯 설정
    private void SetStates(CharacterStates charState)
    {
        maxHealth = charState.maxHealth; // 최대 체력 설정
        health = maxHealth;              // 초기 체력 설정

        restorePerSec += charState.restorePerSec; // 초당 회복량 설정
        defense += charState.defense;             // 방어력 설정
        speed = 1;
        speed = speed * (charState.speed / 100);  // 이동 속도 설정

        attackDamage = 1;
        attackRange = 1;
        attackDamage = attackDamage * (charState.attackDamage / 100); // 공격력 설정
        attackRange = attackRange * (charState.attackRange / 100); // 공격 범위 설정

        abilityHaste = abilityHaste * (charState.abilityHaste / 100); // 능력 가속 (쿨감) 설정
        magnetism += charState.magnetism;                             // 자성 설정

        curse += charState.curse; // 저주 설정
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