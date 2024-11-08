using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/SkillStates", fileName = "SkillStates")]

public class SkillStates : ScriptableObject
{
    public string skillName; // 스킬 이름
    public bool skillON; // 스킬 업그레이드를 했는지

    public void Intialize()
    {
        skillON = false;
    }
}