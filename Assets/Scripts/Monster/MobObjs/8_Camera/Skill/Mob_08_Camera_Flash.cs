using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Mob_08_Camera_Flash : MonoBehaviour, IMobSkill
{

    public MobSkillData skillData; // 스킬 데이터
    public MobSkillData data { get; set; } // 스킬 데이터

    public bool coolDown { get; set; } // 현재 쿨타임


    GameObject obj;
    SpriteRenderer ParRen;
    SpriteRenderer ChaRen;
    bool isEnabled = false;

    public void Init()
    {

        data = skillData;

    }

    public void Use(MobAI AI)
    {

        obj = Instantiate(data.SkillEffect, AI.gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, -90)), AI.transform);

        obj.transform.localPosition = new Vector3(-2, 0.45f);

        ChaRen = obj.GetComponent<SpriteRenderer>();
        ParRen = AI.GetComponent<SpriteRenderer>();

        ChaRen.flipX = ParRen.flipX;
        obj.transform.localPosition = new Vector3(ParRen.flipX ? 2 : -2, 0.45f);
        obj.transform.localScale = new Vector3(3, ParRen.flipX ? -4 : 4);

        isEnabled = true;

        StartCoroutine(Remove(AI));

    }

    private void Update()
    {

        if(isEnabled)
        {

            ChaRen.flipX = ParRen.flipX;
            obj.transform.localPosition = new Vector3(ParRen.flipX ? 2 : -2, 0.45f);
            obj.transform.localScale = new Vector3(3, ParRen.flipX ? -4 : 4);
       
        }

    }

    private IEnumerator Remove(MobAI AI)
    {

        AI.isUsingSkillState = true;

        yield return new WaitForSeconds(0.5f);

        ChaRen.enabled = false;

        obj.GetComponent<PolygonCollider2D>().enabled = true;
        Light2D light = obj.GetComponent<Light2D>();

        light.enabled = true;
        DOTween.To(() => light.intensity, x => light.intensity = x, 8f, 0.25f);

        yield return new WaitForSeconds(0.25f);

        DOTween.To(() => light.intensity, x => light.intensity = x, 0f, 0.25f);

        yield return new WaitForSeconds(0.25f);

        isEnabled = false;
        Destroy(obj);

        AI.isUsingSkillState = false;

    }

}
