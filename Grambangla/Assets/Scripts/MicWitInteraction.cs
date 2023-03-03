using System.Net;
using Meta.WitAi.Json;
using Meta.WitAi;
using TMPro;
using UnityEngine;
public class MicWitInteraction : MonoBehaviour
{
    Wit wit;
    HandleWitResponse handleWitResponse;
    public GameObject recordingButton;
    public GameObject tryAgainTxt;

    [SerializeField] GameObject tutPanel;


   private void OnValidate()
    {
        wit = GetComponent<Wit>();
        handleWitResponse = GetComponent<HandleWitResponse>();

    }
    private void OnEnable()
    {
        wit.VoiceEvents.OnRequestCreated.AddListener(OnRequestStarted);
    }

    private void OnDisable()
    {
        wit.VoiceEvents.OnRequestCreated.RemoveListener(OnRequestStarted);
    }

    private void OnRequestStarted(WitRequest request)
    {
        // The raw response comes back on a different thread. We store the
        // message received for display on the next frame.
        request.onResponse += (r) =>
        {
            if (r.StatusCode == (int)HttpStatusCode.OK)
            {
                handleWitResponse.OnResponse(r.ResponseData);
            }
            else
            {
                Debug.LogError($"Error {r.StatusCode}"+ r.StatusDescription);
            }
        };
    }

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