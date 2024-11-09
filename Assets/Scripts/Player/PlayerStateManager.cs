using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이거 가져가기만 하면 됨
public class PlayerStateManager : MonoBehaviour
{
    public ChaStateManager[] characterStates = new ChaStateManager[3]; // 캐릭터 스탯 배열

    public int currentIndex = 0; // 현재 캐릭터 인덱스 번호

    public GameObject SpaceMarine;
    public GameObject Beeper;
    public GameObject Baz;

    #region 이건 오류 안나는 용도
    public float maxHealth { get; private set; } // 최대 체력
    public float restorePerSec { get; private set; }  // 초당 회복량
    public float defense { get; private set; }  // 방어력
    public float speed { get; private set; }  // 이동 속도 (%)
    public float attackDamage { get; private set; }  // 공격력 (%)
    public float attackRange { get; private set; }   // 공격 범위 (%)
    public float abilityHaste { get; private set; }    // 능력 가속 (쿨감, %)
    public float magnetism { get; private set; }      // 자성
    public float curse { get; private set; }       // 저주
    #endregion

    private void Start()
    {
        // 플레이어의 현재 코인...을 받아올 필요는 없으나? 저장할 때 쓰라고
        //DataManager.Instance.player.coins = currentCoins;

        DataManager.Instance.LoadPlayerData();
        currentIndex = DataManager.Instance.player.currentIndex;

        switch(currentIndex)
        {
            case 0:
                SpaceMarine.SetActive(true);
                Beeper.SetActive(false);
                Baz.SetActive(false);
                break;
            case 1:
                SpaceMarine.SetActive(false);
                Beeper.SetActive(true);
                Baz.SetActive(false);
                break;
            case 2:
                SpaceMarine.SetActive(false);
                Beeper.SetActive(false);
                Baz.SetActive(true);
                break;
        }

        // 플레이어 스탯 + 캐릭터 보너스 스탯 한방에 가져오기
        characterStates[currentIndex].InitializeStats();
        characterStates[currentIndex].GetStats();
        SetStateDRAFT();
    }

    public void SetStateDRAFT()
    {
        maxHealth = characterStates[currentIndex].stateList[0]; // 최대 체력
        restorePerSec = characterStates[currentIndex].stateList[1];   // 초당 회복량
        defense = characterStates[currentIndex].stateList[2];  // 방어력
        speed = characterStates[currentIndex].stateList[3];  // 이동 속도 (%)
        attackDamage = characterStates[currentIndex].stateList[4];   // 공격력 (%)
        attackRange = characterStates[currentIndex].stateList[5];  // 공격 범위 (%)
        abilityHaste = characterStates[currentIndex].stateList[6];  // 능력 가속 (쿨감, %)
        magnetism = characterStates[currentIndex].stateList[7];  // 자성
        curse = characterStates[currentIndex].stateList[8]; // 저주

        if (currentIndex == 1) 
        {
            // 비퍼는 회복을 못쓴다고 하더라고요? 
            restorePerSec = 0;

            // 이후 업그레이드로도 작동하면 안되는데 그건 뭐 나중에...
        }
    }
}
