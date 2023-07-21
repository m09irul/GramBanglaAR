using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{

    [SerializeField] GameObject recordButton;
    [SerializeField] GameObject tutPanel;
    [SerializeField] GameObject rainPrefab;
    [SerializeField] List<AudioClip> s1AudioClips;
    [SerializeField] List<AudioClip> s2AudioClips;

    AudioSource audioSource;
    Animator animator;

    private void Start()
    {
        audioSource =  GetComponent<AudioSource>();
        animator = ObjectSpawner.instance.mainCharacterForScene1.transform.GetChild(0).GetComponent<Animator>();
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
    public void DisappearButtons()
    {
        recordButton.SetActive(false);
        TextManager.instance.ResetDisplayTexts();

    }

    public IEnumerator PlayS1C1()
    {
        OnPlayConvo("G_1_1_Hi_Tomake", s1AudioClips[0]);

        yield return new WaitForSeconds(1.75f);
        ObjectSpawner.instance.lookCharScene1.doLookAtCamera = true;

    }
    public void PlayS1C2()
    {
        OnPlayConvo("G_1_2_Amar_Naam_001", s1AudioClips[1]);
    }
    public void PlayS1C3()
    {
        OnPlayConvo("G_1_3_Montir_Bari_001", s1AudioClips[2]);
    }
    public void PlayS2C1() // scene 2 starts here
    {
        animator = ObjectSpawner.instance.mainCharacterForScene2.transform.GetChild(0).GetComponent<Animator>();
        OnPlayConvo("G_2_1 Ei Dekho Ei Ekta Murgi_001", s2AudioClips[0]);
    }
    public void PlayS2C2()
    {
        OnPlayConvo("G_2_2 Grihopalito", s2AudioClips[1]);
    }
    public void PlayS2C3()
    {
        OnPlayConvo("G_2_3 Grihopalito", s2AudioClips[2]);
    }
    public void PlayS2C4()
    {
        OnPlayConvo("G_2_4_ Pare kintu kom", s2AudioClips[3]);
    }
    public void PlayS2C5()
    {
        OnPlayConvo("G_2_5 Mangsho and Dim", s2AudioClips[4]);
    }
    public void PlayS2C6()
    {
        OnPlayConvo("G_2_6 Ogula palok", s2AudioClips[5]);
    }
    public void PlayS2C7()
    {
        OnPlayConvo("G_2_7 Khelna mukhosh", s2AudioClips[6]);
    }
    public void PlayS2C8()
    {
        OnPlayConvo("G_2_8 Hash O emon", s2AudioClips[7]);
    }
    public void PlayS2C9()
    {
        OnPlayConvo("G_2_9 Murgi Satar Jane nA", s2AudioClips[8]);
    }
    public void PlayS2C10()
    {
        OnPlayConvo("G_2_10 Darao Lali ami aschi", s2AudioClips[9]);
    }
    public void PlayS2C11()
    {
        OnPlayConvo("G_2_11 Lali holo goru", s2AudioClips[10]);
    }
    public void PlayS2C12()
    {
        OnPlayConvo("G_2_12 Notun Bonduh", s2AudioClips[11]);
    }
    public void PlayS2C13()
    {
        OnPlayConvo("G_2_13 Er age goru dekhecho", s2AudioClips[12]);
    }
    public void PlayS2C14()
    {
        OnPlayConvo("G_2_14 Lej diye machi tarai", s2AudioClips[13]);
    }
    public void PlayS2C15()
    {
        OnPlayConvo("G_2_15 Goru ghash khai", s2AudioClips[14]);
    }
    public void PlayS2C16()
    {
        OnPlayConvo("G_2_16 Trinobhoji prani", s2AudioClips[15]);
    }
    public void PlayS2C17()
    {
        OnPlayConvo("G_2_17 Chagol er sathe porichoy", s2AudioClips[16]);
    }
    public void PlayS2C18()
    {
        OnPlayConvo("G_2_18 Mil ache", s2AudioClips[17]);
    }
    public void PlayS2C19()
    {
        OnPlayConvo("G_2_19_ Dudh and mangsho", s2AudioClips[18]);
    }
    public void PlayS2C20()
    {
        OnPlayConvo("G_2_20 monty ke khuje dekhi", s2AudioClips[19]);
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
