using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_05_LoudSpeaker_Speed_Coll : MonoBehaviour
{

    private void Start()
    {

        StartCoroutine(Remove(gameObject));

        gameObject.transform.DOScale(new Vector3(3, 2.5f, 0), 1);

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        
        if(coll.tag == "Enemy")
        {

            MobAI obj = coll.GetComponent<MobAI>();

            if (obj != null && obj.obj.mobName != "확성기")
                obj.SetSpeed(obj.speed * 1.2f);

        }

    }

    private IEnumerator Remove(GameObject obj)
    {

        yield return new WaitForSeconds(1f);

        gameObject.transform.DOScale(new Vector3(0, 0, 0), 1);

        yield return new WaitForSeconds(1f);

        Destroy(obj);

    }

}