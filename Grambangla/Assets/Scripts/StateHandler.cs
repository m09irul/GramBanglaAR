using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{

    [SerializeField] GameObject recordButton;
    [SerializeField] GameObject tutPanel;
    [SerializeField] GameObject rainPrefab;
    [SerializeField] AudioClip c1;
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

    public IEnumerator AppearButtons(float time)
    {
        if(time == 0)
            tutPanel.SetActive(true);

        yield return new WaitForSeconds(time);

        animator.Play("G_Idle_Combined");
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
    public IEnumerator PlayC1()
    {
        OnPlayConvo("G_1_1_Hi_Tomake", c1);

        yield return new WaitForSeconds(1.75f);
        LeanTween.rotateY(ObjectSpawner.instance.mainCharacter, -180f, 1f);
    }
    public void PlayC2()
    {
        OnPlayConvo("G_1_2_Amar_Naam_001", c2);
    }
    public void PlayC3()
    {
        OnPlayConvo("G_1_3_Montir_Bari_001", c3);
    }
    private void OnPlayConvo(string anim, AudioClip clip)
    {
        animator.Play(anim); 
        audioSource.PlayOneShot(clip);
        StartCoroutine(AppearButtons(clip.length));
    }

    public void EndGame()
    {
        Application.Quit(1);
    }

}
