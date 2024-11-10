using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/MobSkillData")]
public class MobSkillData : ScriptableObject
{

    public string skillName; // 스킬 이름

    public MobSkillTag skillTag; // 스킬 태그

    public float coolTime; // 스킬 쿨타임

    public GameObject SkillEffect; // 스킬 이펙트

    public bool isStartCooldown; // 초기화 시 쿨다운 여부

}
