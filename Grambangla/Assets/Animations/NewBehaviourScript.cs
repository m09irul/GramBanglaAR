using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator animator;
    public AudioSource asource;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            animator.Play("a");
            asource.Play();
        }
    }
}
