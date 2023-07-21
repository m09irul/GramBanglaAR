using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(RepeatAudio());
    }
    IEnumerator RepeatAudio()
    {
        audioSource.PlayOneShot(clip);

        yield return new WaitForSeconds(Random.Range(clip.length, clip.length + 10));
    }
}
