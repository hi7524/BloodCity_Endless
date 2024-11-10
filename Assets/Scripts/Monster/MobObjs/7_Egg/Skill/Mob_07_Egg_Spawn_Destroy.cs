using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_07_Egg_Spawn_Destroy : MonoBehaviour
{

    public void StartRemove()
    {

        StartCoroutine(Remove());

    }

    private IEnumerator Remove()
    {

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);

    }

}