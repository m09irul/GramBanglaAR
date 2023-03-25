using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class MenuPikluManager : MonoBehaviour
{
    Animator animator;
    public GameObject Jetpack;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Hi()
    {
        animator.Play("Hi");
    }
    public void FlyOff()
    { 
        animator.Play("FlyOff");
    }
    public void LookAtCam()
    {
        LeanTween.rotateY(gameObject, -180f, 1f);
    }
}
