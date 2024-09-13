using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum AI_MoveType { Normal, Distance }; // 이동 유형

public enum AI_AttackType { Simple, Skill }; // 공격 유형


[CreateAssetMenu(menuName = "Scriptable/MobObj")]
public class MobObj : ScriptableObject
{

    [Header("Basic")]
    public string mobName; // 몬스터 이름
    public int[] dropExp = { 0, 0 }; //  드랍 경험치


    [Header("AI_Type")]
    public AI_MoveType MoveType; // 이동 유형
    public AI_AttackType AttackType; // 공격 유형


    [Header("AI_Propertys")]
    public float Attack_Range; //  공격 유형 : 공격 사거리
    public float Attack_Speed; //  공격 유형 : 공격 속도   

    public float Distance_Range; // 이동 유형 : 거리 조절 - 접근 거리



    [Header("States")]
    public float minHealth;   // 최소 체력
    public float maxHealth;   // 최대 체력

    public float attackDamage; // 공격력

    public float speed;  // 이동 속도


    [Header("Abilitys")]
    public IMobSkill MobAttack; // 몬스터 기본 공격 스크립트
    public IMobSkill MobSkill; // 몬스터 스킬 스크립트
    public IMobSkill MobDeadEvent; // 몬스터 사망 시 이벤트 스크립트

}
