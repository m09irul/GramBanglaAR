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
			else if (intent_Name.Equals("is_bird"))
				HandleResponse(intent_Confidence, 0.97f, 5);

			else if (intent_Name.Equals("domestic_mean"))
				HandleResponse(intent_Confidence, 0.9f, 6);
			else if (intent_Name.Equals("can_fly"))
				HandleResponse(intent_Confidence, 0.97f, 7);
			else if (intent_Name.Equals("say_then"))
				HandleResponse(intent_Confidence, 0.97f, 8);
			else if (intent_Name.Equals("feather"))
				HandleResponse(intent_Confidence, 0.97f, 9);

			else if (intent_Name.Equals("feather_usage"))
				HandleResponse(intent_Confidence, 0.9f, 10);
			else if (intent_Name.Equals("what_use"))
				HandleResponse(intent_Confidence, 0.97f, 11);
			else if (intent_Name.Equals("Who_lali"))
				HandleResponse(intent_Confidence, 0.97f, 12);
			else if (intent_Name.Equals("okay_go"))
				HandleResponse(intent_Confidence, 0.97f, 13);

			else if (intent_Name.Equals("saw"))
				HandleResponse(intent_Confidence, 0.9f, 14);
			else if (intent_Name.Equals("ki_kore"))
				HandleResponse(intent_Confidence, 0.97f, 15);
			else if (intent_Name.Equals("what_eat"))
				HandleResponse(intent_Confidence, 0.97f, 16);
			else if (intent_Name.Equals("vegiterian"))
				HandleResponse(intent_Confidence, 0.97f, 17);

			else if (intent_Name.Equals("has_goat"))
				HandleResponse(intent_Confidence, 0.9f, 18);
			else if (intent_Name.Equals("okay"))
				HandleResponse(intent_Confidence, 0.97f, 19);
			else if (intent_Name.Equals("goat"))
				HandleResponse(intent_Confidence, 0.97f, 20);
			else if (intent_Name.Equals("cow_goat"))
				HandleResponse(intent_Confidence, 0.97f, 21);

			else if (intent_Name.Equals("done"))
				HandleResponse(intent_Confidence, 0.9f, 22);

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
			NextAnim(1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
			NextAnim(2);
		else if (Input.GetKeyDown(KeyCode.Alpha3))
			NextAnim(3);
		else if (Input.GetKeyDown(KeyCode.Alpha4))
			NextAnim(4);
		else if (Input.GetKeyDown(KeyCode.Alpha5))
			NextAnim(5);
		else if (Input.GetKeyDown(KeyCode.Alpha6))
			NextAnim(6);
		else if (Input.GetKeyDown(KeyCode.Alpha7))
			NextAnim(7);
		else if (Input.GetKeyDown(KeyCode.Alpha8))
			NextAnim(8);
	}

	void NextAnim(int which)
	{
		if (which == 1)
			StartCoroutine(objectSpawner.stateHandler.PlayS1C1());
		else if (which == 2)
			objectSpawner.stateHandler.PlayS1C2();
		else if (which == 3)
			objectSpawner.stateHandler.PlayS1C3();
		else if (which == 4)
			objectSpawner.Spawn2ndScene();

	}
}
