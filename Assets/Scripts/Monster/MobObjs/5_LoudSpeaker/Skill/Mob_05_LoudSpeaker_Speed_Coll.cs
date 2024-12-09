using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        
        if(coll.tag == "Enemy" && coll.GetComponent<Mob_05_LoudSpeaker_Speed_Add>() == null)
        {

            coll.GetComponent<MobAI>().AddComponent<Mob_05_LoudSpeaker_Speed_Add>();

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