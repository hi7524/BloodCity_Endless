using UnityEngine;

// 플레이어 캐릭터의 스탯 정보를 적용
public class PlayerCharacterState : Character
{
    public CharacterStates charState;

    private void Awake()
    {
        Debug.Log("선택 캐릭터: " + charState.name);
        SetStates(charState); // 플레이어 캐릭터 스탯 셋업
    }
}