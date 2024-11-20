using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_11_Bus_Dead : MonoBehaviour, IMobSkill
{

    public MobSkillData skillData; // 스킬 데이터
    public MobSkillData data { get; set; } // 스킬 데이터

    public bool coolDown { get; set; } // 현재 쿨타임

    public void Init()
    {
        data = skillData;
    }

    public void Use(MobAI AI)
    {

        StartCoroutine(Delay(AI));

    }

    private IEnumerator Delay(MobAI AI)
    {

        AI.GetComponent<Animator>().Play("11_Bus_01_Dead");

        yield return new WaitForSeconds(2f);

        AI.render.DOColor(Color.clear, 2f);

        yield return new WaitForSeconds(2f);

        AI.Dead();

        UIManager.Instance.ToggleWindow(UIManager.Instance.gameWinWindow);
    }

}