using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRAFT : MonoBehaviour
{
    public float speed = 15;
    private void Update()
    {
        transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), speed * Time.deltaTime);
    }

}
