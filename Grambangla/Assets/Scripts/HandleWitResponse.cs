using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using Meta.WitAi.Json;

public class HandleWitResponse : MonoBehaviour
{
    MicWitInteraction micWitInteraction;
    public AudioClip debugAudio;
	[SerializeField] GameObject endPanel;
	[SerializeField] GameObject rainPrefab;
	[SerializeField] GameObject character;
	ObjectSpawner objectSpawner;

    private void Start()
    {
        micWitInteraction = GetComponent<MicWitInteraction>();
		objectSpawner = ObjectSpawner.instance;
	}
	public void OnResponse(WitResponseNode response)
	{
		if (!string.IsNullOrEmpty(response["text"]))
		{

			float intent_Confidence = float.Parse(response["intents"][0]["confidence"].Value);
			string intent_Name = response["intents"][0]["name"].Value.ToLower();
			string userSpoken_text = response["text"];

			Debug.LogError(intent_Confidence);
			Debug.LogError(intent_Name);
			Debug.LogError(userSpoken_text);
			//Debug.Log("I heard: " + response[""]);

			// what function should I call?
			if (intent_Name.Equals("hello"))
				HandleResponse(intent_Confidence, 0.97f, 1);
			else if (intent_Name.Equals("ask_name"))
				HandleResponse(intent_Confidence, 0.9f, 2);
			else if (intent_Name.Equals("ask_where_going"))
				HandleResponse(intent_Confidence, 0.97f, 3);
			else if (intent_Name.Equals("lets_go"))
				HandleResponse(intent_Confidence, 0.97f, 4);
			//Scene 1 donr above.
			//scene 2 below..

			else
			{
				micWitInteraction.HandleException();
			}
		}
		else
		{
			micWitInteraction.HandleException();
		}
	}

    private void HandleResponse(float intent_Confidence, float threshold, int index)
    {
        if (intent_Confidence >= threshold)
        {
            micWitInteraction.recordingButton.SetActive(false);
            TextManager.instance.ResetDisplayTexts();

            NextAnim(index);
        }
        else
            micWitInteraction.HandleException();
    }

    public void OnError(string error, string message)
	{
		micWitInteraction.HandleException();
	}

	public void OnErrorDueToInactivity()
	{
		micWitInteraction.HandleException();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			//character.GetComponent<Animator>().Play("start");
			character.transform.GetChild(0).GetComponent<Animator>().enabled = true;
			AudioSource s = character.GetComponent<AudioSource>();
			s.clip = debugAudio;
			s.Play();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
			NextAnim(2);
		else if (Input.GetKeyDown(KeyCode.Alpha3))
			NextAnim(3);
		else if (Input.GetKeyDown(KeyCode.Alpha5))
			NextAnim(1);
		else if (Input.GetKeyDown(KeyCode.Alpha6))
			NextAnim(2);
		else if (Input.GetKeyDown(KeyCode.Alpha7))
			NextAnim(3);
		else if (Input.GetKeyDown(KeyCode.Alpha8))
			NextAnim(4);
	}

	void NextAnim(int which)
	{
		if (which == 1)
			StartCoroutine(objectSpawner.stateHandler.PlayC1());
		else if (which == 2)
			objectSpawner.stateHandler.PlayC2();
		else if (which == 3)
			objectSpawner.stateHandler.PlayC3();
		else if (which == 4)
			objectSpawner.Spawn2ndScene();

	}
}
