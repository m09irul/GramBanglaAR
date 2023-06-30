using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using UnityEngine.Networking;
using GleyInternetAvailability;
using Dreamteck.Splines;

[System.Serializable]
public class ToSpawnObject
{
    public GameObject objectsToSpawn;
    public GameObject VFX;
    public Vector3 VFXScale;
}
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
    [SerializeField] Vector3 afterPlacementOffset;

    public bool canSpawn = false;
    bool hasInternet = false;

    [SerializeField] GameObject tapToPlaceTxt;
    [SerializeField] ARPlaneManager aRPlaneManager;
    [SerializeField] AudioClip startAudio;
    public GameObject mainCharacter;
    [SerializeField] GameObject spawnVFXForScene1;
    [SerializeField] GameObject noInternetPanel;
    public StateHandler stateHandler;
    public List<ToSpawnObject> toSpawnObjects;
    Vector3 spawnPoint;
    Quaternion spawnRot;

    public void SetSpawnPoint(Vector3 pos, Quaternion rot)
    {
        spawnPoint = pos;
        spawnRot = rot;

        objectToSpawn.transform.position = spawnPoint;
        objectToSpawn.transform.rotation = spawnRot;
    }
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

        StartCoroutine(LookForInternetConnection());

        var sf = mainCharacter.GetComponent<SplineFollower>();
        sf.onEndReached += (o) =>
        {
            StartCoroutine(stateHandler.AppearButtons(0));
            sf.follow = false;
        };
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

            setScene();
        }
    }

    void setScene()
    {
        AudioSource s = GetComponent<AudioSource>();
        s.Play();

        GameObject particle = Instantiate(spawnVFXForScene1, objectToSpawn.transform.position, Quaternion.identity);
        Destroy(particle, 5f);

        LeanTween.scale(objectToSpawn, Vector3.one, 0.7f).setEase(LeanTweenType.easeOutElastic).setOnComplete(() =>
        {
            LeanTween.value(gameObject, 0, 1, 1f).setOnComplete(() =>
            {
                foreach (var item in toSpawnObjects)
                {
                    item.objectsToSpawn.SetActive(true);

                    if (item.objectsToSpawn.transform.childCount > 0)
                    {
                        for (int i = 0; i < item.objectsToSpawn.transform.childCount; i++)
                        {
                            GameObject tmpParticle = Instantiate(item.VFX, item.objectsToSpawn.transform.GetChild(i).transform.position, Quaternion.identity);
                            tmpParticle.transform.localScale = item.VFXScale;
                            Destroy(tmpParticle, 5f);
                        }
                    }
                    else
                    {
                        GameObject tmpParticle = Instantiate(item.VFX, item.objectsToSpawn.transform.position, Quaternion.identity);
                        tmpParticle.transform.localScale = item.VFXScale;
                        Destroy(tmpParticle, 5f);
                    }
                }
            });
        });
        //objectToSpawn.SetActive(true);

        //objectToSpawn.transform.position = spawnPoint + afterPlacementOffset;
        //objectToSpawn.transform.rotation = spawnRot;
        //objectToSpawn.transform.LookAt(Camera.main.transform);


        //GameObject particle =  Instantiate(spawnVFX, spawnPoint, Quaternion.identity);
        //Destroy(particle, 5f);

        //scale well
        /*        LeanTween.scale(objectToSpawn.transform.GetChild(0).GetChild(4).gameObject, Vector3.one*10f, 2f).setEase(LeanTweenType.easeOutQuad).setOnComplete(()=>
                {
                    mainCharacter.SetActive(true);

                    //bring fences, rock and flower from downward
                    LeanTween.moveLocalZ(objectToSpawn.transform.GetChild(0).GetChild(1).gameObject, 0f, 12f).setEase(LeanTweenType.easeOutCirc);

                    //bring house, tracktor and water tank from upward
                    LeanTween.moveLocalZ(objectToSpawn.transform.GetChild(0).GetChild(2).gameObject, 0f, 7f).setEase(LeanTweenType.easeOutExpo);

                    //grow trees
                    LeanTween.scaleZ(objectToSpawn.transform.GetChild(0).GetChild(3).gameObject, 1f, 15f).setEase(LeanTweenType.easeOutCirc);
                });*/


        tapToPlaceTxt.SetActive(false);

    }
}
    
