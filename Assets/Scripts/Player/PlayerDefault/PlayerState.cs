using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //DataManager.Instance.LoadPlayerData();
        //currentIndex = DataManager.Instance.player.currentIndex;

        // 플레이어 스탯 + 캐릭터 보너스 스탯 한방에 가져오기
        //characterStates[currentIndex].InitializeStats();
        //characterStates[currentIndex].GetStats();
        SetStateDRAFT();
    }

    // 예진이가 연결할 때 쓸 메서드!!!!!
    public void SetState()
    {
        /*maxHealth = characterStates[currentIndex].stateList[0]; // 최대 체력
        restorePerSec = characterStates[currentIndex].stateList[1];   // 초당 회복량
        defense = characterStates[currentIndex].stateList[2];  // 방어력
        speed = characterStates[currentIndex].stateList[3];  // 이동 속도 (%)
        attackDamage = characterStates[currentIndex].stateList[4];   // 공격력 (%)
        attackRange = characterStates[currentIndex].stateList[5];  // 공격 범위 (%)
        abilityHaste = characterStates[currentIndex].stateList[6];  // 능력 가속 (쿨감, %)
        magnetism = characterStates[currentIndex].stateList[7];  // 자성
        curse = characterStates[currentIndex].stateList[8]; // 저주*/

        if (currentIndex == 1)
        {
            // 비퍼는 회복을 못쓴다고 하더라고요? 
            restorePerSec = 0;

            // 이후 업그레이드로도 작동하면 안되는데 그건 뭐 나중에...
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
        
        // 스타트씬으로 갔을 때 선택 창 띄우려고 추가함
        //DataManager.Instance.player.PlayerRestart = true;
        //DataManager.Instance.Save();

        // 추후 업적 창 나오면 그거 버튼에다 옮길 예정
        // SceneManager.LoadScene("StartScene");
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