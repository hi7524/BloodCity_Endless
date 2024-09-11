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

    // 스킬 (강화를 통해 얻는 스킬)
    public bool passiveSkill;    // 플레임 벨트
    public bool fragmentGrenade; // 파편 수류탄
    public bool iceGrenade;      // 얼음 수류탄



    // 선택한 캐릭터
    // 레벨
    public int playerLevel = 0;
    // 현재 획득 경험치
    // 클리어 한 스테이지 (스테이지 해금 여부)
    // 영구 강화 스탯
    // 코인

    public void UnlockSkill(int num)
    {

    }
}

