using UnityEngine;

public class EnhanceState : ScriptableObject
{
    public string stateName; // 스탯 이름
    public float value; // 스탯의 기본값

    public StateUpgrade[] stateUpgrade = new StateUpgrade[3]; // 얼마나 업그레이드 되는지

    public int enhanceLevel = 0; // 현재 단계
    public bool isUpgraded = false; // 업그레이드 여부

    public void Initialize()
    {
        enhanceLevel = 0; // 기본값
        isUpgraded = false; // 기본값
    }

    // 아직 필요없음
    public void DisplayUpgradeOptions()
    {
        for (int i = 0; i < stateUpgrade.Length; i++)
        {
            if (i < enhanceLevel)
            {
                Debug.Log($"업그레이드 {i + 1}: {stateUpgrade[i].GetDescription()}");
            }
        }
    }

    public float GetEnhancedValue()
    {
        float enhancedValue = value; // 기본 값으로 초기화

        // 현재 업그레이드 레벨에 따라 증가량 적용
        for (int i = 0; i < enhanceLevel; i++)
        {
            if (i < stateUpgrade.Length) // 업그레이드 배열의 범위 내에서
            {
                if (stateUpgrade[i].isPercentage)
                {
                    enhancedValue += enhancedValue * (stateUpgrade[i].increaseAmount / 100); // 퍼센트 계산
                }
                else
                {
                    enhancedValue += stateUpgrade[i].increaseAmount; // 정수 증가
                }
            }
        }

        return enhancedValue; // 최종 값 반환
    }
}


[System.Serializable]
public class StateUpgrade
{
    public float increaseAmount; // 증가량 (퍼센트 또는 정수)
    public float cost; // 업그레이드 비용
    public string description; // 스탯 설명
    public bool isPercentage; // 증가 방식 (true: 퍼센트, false: 정수)

    public StateUpgrade(float increaseAmount, float cost, string description, bool isPercentage)
    {
        this.increaseAmount = increaseAmount;
        this.cost = cost;
        this.description = description;
        this.isPercentage = isPercentage;
    }

    public string GetDescription()
    {
        return $"{description} {increaseAmount}{(isPercentage ? "%" : "")} 증가시킵니다.";
    }
}
