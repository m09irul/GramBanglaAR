using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimations : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(PlayAnimations(Random.Range(1,5).ToString()));
    }
    IEnumerator PlayAnimations(string animName)
    {
        animator.Play(animName);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        StartCoroutine(PlayAnimations(Random.Range(1, 5).ToString()));
    }
}
