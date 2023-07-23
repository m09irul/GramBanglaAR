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
	public MicWitInteraction mwi;
	ObjectSpawner objectSpawner;
	int index;
	public string[] intents;
    private void Start()
    {
		index = 0;
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
			mwi.textArea.text = intent_Confidence.ToString() + "     " + intent_Name + "     " + userSpoken_text;
			//Debug.Log("I heard: " + response[""]);
			Debug.Log(intents[index]);
			Debug.Log(intent_Name);
			if (intents[index].Equals(intent_Name))
			{
				HandleResponse(intent_Confidence, 0.5f, index + 1);
				index++;
				Debug.LogError("inside: " + intent_Name);
			}
			else
			{
				Debug.Log("bal");
				micWitInteraction.HandleException();
			}
		}
		else
		{
			print("shawa");
			micWitInteraction.HandleException();
		}
	}

    private void HandleResponse(float intent_Confidence, float threshold, int index)
    {
        if (intent_Confidence >= threshold)
        {
			Debug.LogError("inside: ");
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
		else if (Input.GetKeyDown(KeyCode.Alpha9))
			NextAnim(9);
		else if (Input.GetKeyDown(KeyCode.Q))
			NextAnim(10);
		else if (Input.GetKeyDown(KeyCode.W))
			NextAnim(11);
		else if (Input.GetKeyDown(KeyCode.E))
			NextAnim(12);
		else if (Input.GetKeyDown(KeyCode.R))
			NextAnim(13);
		else if (Input.GetKeyDown(KeyCode.T))
			NextAnim(14);
		else if (Input.GetKeyDown(KeyCode.Y))
			NextAnim(15);
		else if (Input.GetKeyDown(KeyCode.U))
			NextAnim(16);
		else if (Input.GetKeyDown(KeyCode.I))
			NextAnim(17);
		else if (Input.GetKeyDown(KeyCode.O))
			NextAnim(18);
		else if (Input.GetKeyDown(KeyCode.P))
			NextAnim(19);
		else if (Input.GetKeyDown(KeyCode.A))
			NextAnim(20);
		else if (Input.GetKeyDown(KeyCode.S))
			NextAnim(21);

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
		else if (which == 5)
			objectSpawner.stateHandler.PlayS2C2();
		else if (which == 6)
			objectSpawner.stateHandler.PlayS2C3();
		else if (which == 7)
			objectSpawner.stateHandler.PlayS2C4();
		else if (which == 8)
			objectSpawner.stateHandler.PlayS2C5();
		else if (which == 9)
			objectSpawner.stateHandler.PlayS2C6();
		else if (which == 10)
			StartCoroutine(objectSpawner.stateHandler.PlayS2C7());
		else if (which == 11)
			StartCoroutine(objectSpawner.stateHandler.PlayS2C9());
		else if (which == 12)
			objectSpawner.stateHandler.PlayS2C11();
		else if (which == 13)
			objectSpawner.stateHandler.PlayS2C12();
		else if (which == 14)
			objectSpawner.stateHandler.PlayS2C14();
		else if (which == 15)
			objectSpawner.stateHandler.PlayS2C15();
		else if (which == 16)
			objectSpawner.stateHandler.PlayS2C16();
		else if (which == 17)
			objectSpawner.stateHandler.PlayS2C17();
		else if (which == 18)
			objectSpawner.stateHandler.GoToChagol();
		else if (which == 19)
			objectSpawner.stateHandler.PlayS2C18();
		else if (which == 20)
			StartCoroutine(objectSpawner.stateHandler.PlayS2C19());
		else if (which == 21)
			objectSpawner.stateHandler.OnGameFinish();

	}
}
