using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;
using TMPro;
using System.Text;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using UnityEngine.Networking;
using GleyInternetAvailability;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] GameObject objectToSpawn;

    PlacementIndicator placementIndicator;

    [SerializeField] Vector3 afterPlacementScale;

    bool canSpawn = false;
    bool hasInternet = false;

    [SerializeField] GameObject tapToPlaceTxt;
    [SerializeField] ARPlaneManager aRPlaneManager;
    [SerializeField] AudioClip startAudio;
    [SerializeField] GameObject mainCharacter;
    [SerializeField] GameObject spawnVFX;
    [SerializeField] GameObject noInternetPanel;

    Vector3 spawnPoint;

    public void SetSpawnPoint(Vector3 pos)
    {
        spawnPoint = pos;
    }
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

        StartCoroutine(LookForInternetConnection());

    }


    IEnumerator LookForInternetConnection()
    {
        while (true)
        {
            GleyInternetAvailability.Network.IsAvailable(CompleteMethod);
            yield return new WaitForSeconds(10f);
        }


    }

    private void CompleteMethod(ConnectionResult connectionResult)
    {
        if (connectionResult == ConnectionResult.Working)
        {
            noInternetPanel.SetActive(false);
            hasInternet = true;
            //Debug.LogError(hasInternet);
        }
        else
        {
            noInternetPanel.SetActive(true);
            hasInternet = false;
        }
            
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    public IEnumerator ShowMarkerAndReadyToSpawn()
    {
        yield return new WaitForSeconds(3f);

        tapToPlaceTxt.SetActive(true);
        canSpawn = true;
    }
    private void Update()
    {
        SpawnSystem();
    }

    private void SpawnSystem()
    {
        if (((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.Space)) && canSpawn && hasInternet)
        {

            canSpawn = false;

            //turn of placement indicator and spawing..
            aRPlaneManager.enabled = false;

            foreach (ARPlane plane in aRPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }

            placementIndicator.enabled = false;
            placementIndicator.gameObject.SetActive(false);

            objectToSpawn.SetActive(true);

            objectToSpawn.transform.position = spawnPoint;
            GameObject particle =  Instantiate(spawnVFX, spawnPoint, Quaternion.identity);
            Destroy(particle, 5f);

            //scale up
            //objectToSpawn.transform.DOScale(afterPlacementScale, 1f);

            

            tapToPlaceTxt.SetActive(false);


            StartCoroutine(startScene());

        }
    }

    IEnumerator startScene()
    {
        yield return new WaitForSeconds(2f);

        //mainCharacter.GetComponent<Animator>().Play("start");
        mainCharacter.transform.GetChild(0).GetComponent<Animator>().enabled = true;
        AudioSource s = mainCharacter.GetComponent<AudioSource>();
        s.clip = startAudio;
        s.Play();
    }



}
