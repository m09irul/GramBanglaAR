                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using TMPro;
public class MicWitInteraction : MonoBehaviour
{
    Wit wit;

    public GameObject recordingButton;
    public GameObject tryAgainTxt;

    [SerializeField] GameObject tutPanel;


    private void Start()
    {
        wit = GetComponent<Wit>();

    }
    /*public void ToggleActivation()
    {
        if (wit.Active) wit.Deactivate();
        else
        {
            wit.Activate();
        }
    }*/

    public void StopRecording()
    {
        try
        {
            wit.Deactivate();
        }
        catch
        {
            HandleException();
        }

    }
    public void StartRecording()
    {
        if (tutPanel.activeInHierarchy)
        {
            tutPanel.SetActive(false);
        }
        //deactive try again buton..
        tryAgainTxt.SetActive(false);

        try
        {
            if (!wit.Active)
                wit.Activate();
        }
        catch
        {
            HandleException();
        }
    }

    public void HandleException()
    {
        tryAgainTxt.SetActive(true);
        recordingButton.SetActive(true);
    }
}                                                                                                                                                                                                                                              