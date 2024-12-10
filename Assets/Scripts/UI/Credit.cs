using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    public GameObject creditObj;
    public Animator animator;

    public void Start()
    {
        creditObj.SetActive(false);
    }

    public void CreditOn()
    {
        UI_StartScene.Instance.pauseWindow.SetActive(false);
        creditObj.SetActive(true);
        animator.Play("Credit");
    }

    public void CreditOff()
    {
        UI_StartScene.Instance.pauseWindow.SetActive(true);
        creditObj.SetActive(false);
    }
}
