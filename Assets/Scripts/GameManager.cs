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

    // 선택 스킬
    public bool playerSkill0 { get; private set; }
    public bool playerSkill1 { get; private set; }
    public bool PlayerSkill2 { get; private set; }


    // 선택한 캐릭터
    // 레벨
    public int playerLevel = 0;
    // 현재 획득 경험치
    // 클리어 한 스테이지 (스테이지 해금 여부)
    // 영구 강화 스탯
    // 코인
}

