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

    string _name = "";

    private void Start()
    {
        micWitInteraction = GetComponent<MicWitInteraction>();
    }

    public void OnResponse(WitResponseNode response)
    {
        if (!string.IsNullOrEmpty(response["text"]))
        {

            float intent_Confidence = float.Parse(response["intents"][0]["confidence"].Value);
            string intent_Name = response["intents"][0]["name"].Value.ToLower();
            string userSpoken_text = response["text"];

            Debug.LogError(intent_Confidence);
            //Debug.LogError(intent_Name);
            Debug.LogError(userSpoken_text);
			//Debug.Log("I heard: " + response[""]);

			// what function should I call?
			if (intent_Name.Equals("hello"))
			{

				if (intent_Confidence >= 0.97f)
				{
					micWitInteraction.recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();

					Debug.Log("Donnnnneeeeeee....");
				}
				else
					micWitInteraction.HandleException();

			}
			else if (intent_Name.Equals("name_intent"))
			{
				_name = userSpoken_text.Substring(9);
				

				if (intent_Confidence >= 0.98f)
				{
					micWitInteraction.recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();
					NextAnim(0);
				}
				else
					micWitInteraction.HandleException();
			}
			else if (intent_Name.Equals("im_fine_intent"))
			{
				

				if (intent_Confidence >= 0.97f)
				{
					//মি.গাছ - কিন্তু আমি ভালো নেই। আমার মন ভীষণ খারাপ।
					micWitInteraction.recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();
					NextAnim(1);
				}
				else
					micWitInteraction.HandleException();
			}
			else if (intent_Name.Equals("asking_sad_intent"))
			{
				

				if (intent_Confidence >= 0.97f)
				{
					//মি.গাছ - সে অনেক কথা! (খুশি হয়ে) আমি এক সময় তোমার মত ছোট ছিলাম। 
					//এখন আমার বয়স ১৫০ বছর। 
					//তুমি কি জানো আমি কিভাবে এতো বড় হলাম?
					micWitInteraction.recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();
					NextAnim(2);
				}
				else
					micWitInteraction.HandleException();
			}
			else if (intent_Name.Equals("asking_getting_big_intent"))
			{
				

				if (intent_Confidence >= 0.95f)
				{
					//মি.গাছ - তাহলে শোনো, আমি যখন খুব ছোট ছিলাম তখন একদিন এক দয়ালু মানুষ আমাকে তার বাড়ির বাগানে বপন করে। 
					micWitInteraction.recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();
					NextAnim(3);
				}
				else
					micWitInteraction.HandleException();
			}
			else if (intent_Name.Equals("asking_getting_big2_intent"))
			{
				

				if (intent_Confidence >= 0.92f)
				{
					//মি.গাছ - না, গাছের বড় হওয়ার যত্ন এবং পানির প্রয়োজন। 

					micWitInteraction.recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();
					NextAnim(4);

					//TAP পানির পাত্র এবং আমাদের আরও পানি দিয়ে বড় হতে সাহায্য করো!
					//মি.গাছ - আহ ধন্যবাদ!
					//জানো আগে আমার সাথে আমার অনেক ভাই - বোন, বন্ধু - বান্ধব ছিল। কিন্তু এখানে আমি একা!তুমি কি জানো তারা এখন কোথায় ?
					//These are handled in stateHandler script..
				}
				else
					micWitInteraction.HandleException();
			}
			else if (intent_Name.Equals("asking_where_intent"))
			{
				

				if (intent_Confidence >= 0.96f)
				{
					//Debug.LogError(5);
					//end
					endPanel.SetActive(true);
					TextManager.instance.ResetDisplayTexts();
					micWitInteraction.recordingButton.SetActive(false);

				}
				else
				{
					//Debug.LogError(0000);
					micWitInteraction.HandleException();
				}
			}
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
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			character.GetComponent<StateHandler>().PlayC3();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
			NextAnim(0);
		else if (Input.GetKeyDown(KeyCode.Alpha5))
			NextAnim(1);
		else if (Input.GetKeyDown(KeyCode.Alpha6))
			NextAnim(2);
		else if (Input.GetKeyDown(KeyCode.Alpha7))
			NextAnim(3);
		else if (Input.GetKeyDown(KeyCode.Alpha8))
			NextAnim(4);
		else if (Input.GetKeyDown(KeyCode.Alpha9))
			character.GetComponent<StateHandler>().PlayC9();
	}

	void NextAnim(int which)
	{
		if (which == 0)
			character.GetComponent<StateHandler>().PlayC4();
		else if (which == 1)
			character.GetComponent<StateHandler>().PlayC5();
		else if (which == 2)
			character.GetComponent<StateHandler>().PlayC6();
		else if (which == 3)
			character.GetComponent<StateHandler>().PlayC7();
		else if (which == 4)
			character.GetComponent<StateHandler>().PlayC8();

	}
}
