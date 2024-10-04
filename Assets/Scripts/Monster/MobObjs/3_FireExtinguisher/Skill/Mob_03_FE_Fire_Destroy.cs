using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_03_FE_Fire_Destroy : MonoBehaviour
{

    public void StartRemove()
    {

        StartCoroutine(Remove());

    }

    private IEnumerator Remove()
    {

        yield return new WaitForSeconds(16.5f);

        Destroy(gameObject);

    }

}