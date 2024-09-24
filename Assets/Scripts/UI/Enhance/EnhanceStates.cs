using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Scriptable/EnhanceStates", fileName = "EnhanceState")]
public class EnhanceState : ScriptableObject
{
    public string stateName; // 스탯 이름
    public float value; // 스탯의 기본값

    public StateUpgrade[] stateUpgrade = new StateUpgrade[3]; // 얼마나 업그레이드 되는지

    public int enhanceLevel = 0; // 현재 단계
    public bool isUpgraded = false; // 업그레이드 여부
}

[System.Serializable]
public class StateUpgrade
{
    public float increaseAmount; // 증가량
    public float cost; // 업그레이드 비용

    public StateUpgrade(float increaseAmount, float cost)
    {
        this.increaseAmount = increaseAmount;
        this.cost = cost;
    }
}