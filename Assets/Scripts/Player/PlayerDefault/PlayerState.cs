using UnityEngine;
using System.IO;

// 플레이어 캐릭터의 스탯 정보를 적용
public class PlayerState : MonoBehaviour
{ 
    public static PlayerState Instance { get; private set; }

    public bool isPlayerDead { get; private set; } // 플레이어 사망 여부
    public float maxHealth { get; set; } // 최대 체력
    public float restorePerSec { get; set; }  // 초당 회복량
    public float defense { get; set; }  // 방어력
    public float speed { get; set; }  // 이동 속도 (%)
    public float attackDamage { get; set; }  // 공격력 (%)
    public float attackRange { get; set; }   // 공격 범위 (%)
    public float abilityHaste { get; set; }    // 능력 가속 (쿨감, %)
    public float magnetism { get; set; }      // 자성
    public float curse { get; set; }       // 저주

    public ChaStateManager[] characterStates = new ChaStateManager[3]; // 캐릭터 스탯 배열

    public int currentIndex = 0; // 현재 캐릭터 인덱스 번호

    public bool[] skillOn;

    PermanentSkillManager PermanentBuffSkill;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            // 만약 오류나면 C:\(사용자)\(유저이름)\AppData\LocalLow\Endless\BloodCity_Endless 에서 PlayerData 지우기
            Debug.Log("플레이어 데이터가 존재합니다");

            DataManager.Instance.LoadPlayerData();
            currentIndex = DataManager.Instance.player.currentIndex;

            // 플레이어 스탯 + 캐릭터 보너스 스탯 한방에 가져오기
            characterStates[currentIndex].InitializeStats();
            characterStates[currentIndex].GetStats();

            SetState();
            skillOn = DataManager.Instance.player.skillON;
            PermanentBuffSkill = this.GetComponentInChildren<PermanentSkillManager>();

            for (int i = 0; i < skillOn.Length; i++)
            {
                if (skillOn[i])
                {
                    PermanentBuffSkill.AddPermanentSkill(i);
                }
            }


        }
        else
        {
            Debug.Log("게임씬에서 테스트 중입니다");
            SetStateDRAFT();
        }

    }

    public void SetState()
    {
        maxHealth = characterStates[currentIndex].stateList[0]; // 최대 체력
        restorePerSec = characterStates[currentIndex].stateList[1];   // 초당 회복량
        defense = characterStates[currentIndex].stateList[2];  // 방어력
        speed = 4 + speed * (characterStates[currentIndex].stateList[3] / 100);  // 이동 속도 (%)
        attackDamage = 1 + attackDamage * (characterStates[currentIndex].stateList[4] / 100);   // 공격력 (%)
        attackRange = 1 + attackRange * (characterStates[currentIndex].stateList[5] / 100);  // 공격 범위 (%)
        abilityHaste = 1 + abilityHaste * (characterStates[currentIndex].stateList[6] / 100);  // 능력 가속 (쿨감, %)
        magnetism = characterStates[currentIndex].stateList[7];  // 자성
        curse = characterStates[currentIndex].stateList[8]; // 저주

        if (currentIndex == 1)
        {
            restorePerSec = 0;
        }
    }

    // 임시로 사용할 때 (연결 해제) 메서드~!!!!
    private void SetStateDRAFT()
    {
        isPlayerDead = false;  // 플레이어 사망 여부
        maxHealth = 100; // 최대 체력
        restorePerSec = 1;   // 초당 회복량
        defense = 1;  // 방어력
        speed = 4f;  // 이동 속도 (%)
        attackDamage = 1;   // 공격력 (%)
        attackRange = 0;  // 공격 범위 (%)
        abilityHaste = 0;  // 능력 가속 (쿨감, %)
        magnetism = 1;  // 자성
        curse = 0; // 저주
    }
    

    public void SetPlayerDead()
    {
        Debug.Log("플레이어 사망");
        isPlayerDead = true;

        Time.timeScale = 0;
    }


/*    // 초기 스탯 설정
    private void SetStates(CharacterStates charState)
    {
        maxHealth = defaultState.maxHealth + charState.maxHealth; // 최대 체력 설정
        health = maxHealth;              // 초기 체력 설정

        restorePerSec = defaultState.restorePerSec + charState.restorePerSec; // 초당 회복량 설정
        defense = defaultState.restorePerSec + charState.defense;             // 방어력 설정
        speed = 1;
        speed = speed * (charState.speed / 100);  // 이동 속도 설정

        attackDamage = 1;
        attackRange = 1;
        attackDamage = attackDamage * (charState.attackDamage / 100); // 공격력 설정
        attackRange = attackRange * (charState.attackRange / 100); // 공격 범위 설정

        abilityHaste = abilityHaste * (charState.abilityHaste / 100); // 능력 가속 (쿨감) 설정
        magnetism += charState.magnetism;                             // 자성 설정

        curse += charState.curse; // 저주 설정
    }*/


}