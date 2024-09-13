using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/MobSkillData")]
public class MobSkillData : ScriptableObject
{

    public string skillName; // ��ų �̸�

    public MobSkillTag skillTag; // ��ų �±�

    public float coolTime; // ��ų ��Ÿ��

    public GameObject SkillEffect; // ��ų ����Ʈ

}
