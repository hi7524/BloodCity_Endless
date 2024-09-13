using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum AI_MoveType { Normal, Distance }; // �̵� ����

public enum AI_AttackType { Simple, Skill }; // ���� ����


[CreateAssetMenu(menuName = "Scriptable/MobObj")]
public class MobObj : ScriptableObject
{

    [Header("Basic")]
    public string mobName; // ���� �̸�
    public int[] dropExp = { 0, 0 }; //  ��� ����ġ


    [Header("AI_Type")]
    public AI_MoveType MoveType; // �̵� ����
    public AI_AttackType AttackType; // ���� ����


    [Header("AI_Propertys")]
    public float Attack_Range; //  ���� ���� : ���� ��Ÿ�
    public float Attack_Speed; //  ���� ���� : ���� �ӵ�   

    public float Distance_Range; // �̵� ���� : �Ÿ� ���� - ���� �Ÿ�



    [Header("States")]
    public float minHealth;   // �ּ� ü��
    public float maxHealth;   // �ִ� ü��

    public float attackDamage; // ���ݷ�

    public float speed;  // �̵� �ӵ�


    [Header("Abilitys")]
    public IMobSkill MobAttack; // ���� �⺻ ���� ��ũ��Ʈ
    public IMobSkill MobSkill; // ���� ��ų ��ũ��Ʈ
    public IMobSkill MobDeadEvent; // ���� ��� �� �̺�Ʈ ��ũ��Ʈ

}
