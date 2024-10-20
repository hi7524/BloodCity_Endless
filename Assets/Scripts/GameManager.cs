using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameManager
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
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

    void Awake()
    {
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


    // 선택한 캐릭터
    // 레벨
    public int playerLevel = 0;
    // 현재 획득 경험치
    // 클리어 한 스테이지 (스테이지 해금 여부)
    // 영구 강화 스탯

    public int coin { get; private set; } // 코인

    // 코인 추가
    public void AddCoin(int addCoin)
    {
        coin += addCoin;
        Debug.Log("Coin: " +  coin);   
        UIManager.Instance.UpdateCoin(coin);
    }

    // 플레이어 레벨업
    public void PlayerLevelUp()
    {
        playerLevel ++; // 레벨업
    }

    public void UnlockSkill(int num)
    {

    }
}

