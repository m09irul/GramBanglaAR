using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{

    [SerializeField] GameObject recordButton;
    [SerializeField] GameObject tutPanel;
    [SerializeField] GameObject rainPrefab;
    [SerializeField] AudioClip c2;
    [SerializeField] AudioClip c3;
    [SerializeField] AudioClip c4;
    [SerializeField] AudioClip c5;
    [SerializeField] AudioClip c6;
    [SerializeField] AudioClip c7;
    [SerializeField] AudioClip c8;
    [SerializeField] AudioClip c9;
    AudioSource audioSource;
    Animator animator;

    private void Start()
    {
        audioSource =  GetComponent<AudioSource>();
        animator = ObjectSpawner.instance.mainCharacter.transform.GetChild(0).GetComponent<Animator>();
    }
    public void AppearButtonsForFirst()
    {
        recordButton.SetActive(true);
        tutPanel.SetActive(true);
        TextManager.instance.LoadNextText();
        animator.Play("Idle");
    }
    public void AppearButtonsForRest()
    {
        recordButton.SetActive(true);
        TextManager.instance.LoadNextText();

    }
    public void TapWaterPot()
    {
        transform.GetChild(10).gameObject.SetActive(true);
        TextManager.instance.LoadNextText();
    }

    void DeactiveOtherModels(int currentModel)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(currentModel).gameObject.SetActive(true);
    }
    public void PlayC2()
    {
        //GetComponent<Animator>().Play("c2");
        DeactiveOtherModels(1);
        transform.GetChild(1).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c2;
        audioSource.Play();
    }
    public void PlayC3()
    {
        //GetComponent<Animator>().Play("c3");
        DeactiveOtherModels(3);
        transform.GetChild(3).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c3;
        audioSource.Play();
    }
    public void PlayC4()
    {
        //GetComponent<Animator>().Play("c4");
        DeactiveOtherModels(4);
        transform.GetChild(4).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c4;
        audioSource.Play();
    }
    public void PlayC5()
    {
        //GetComponent<Animator>().Play("c5");
        DeactiveOtherModels(5);
        transform.GetChild(5).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c5;
        audioSource.Play();
    }
    public void PlayC6()
    {
        //GetComponent<Animator>().Play("c6");
        DeactiveOtherModels(6);
        transform.GetChild(6).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c6;
        audioSource.Play();
    }
    public void PlayC7()
    {
        //GetComponent<Animator>().Play("c7");
        DeactiveOtherModels(7);
        transform.GetChild(7).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c7;
        audioSource.Play();
    }
    public void PlayC8()
    {
        //GetComponent<Animator>().Play("c8");
        DeactiveOtherModels(8);
        transform.GetChild(8).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c8;
        audioSource.Play();
    }
    public void PlayC9()
    {
        //GetComponent<Animator>().Play("c9");
        DeactiveOtherModels(9);
        transform.GetChild(9).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c9;
        audioSource.Play();
    }
    public void PlayStar()
    {
        //GetComponent<Animator>().Play("c9");
        DeactiveOtherModels(2);
        transform.GetChild(2).GetComponent<Animator>().enabled = true;
        
    }

    public void EndGame()
    {
        Application.Quit(1);
    }

}
