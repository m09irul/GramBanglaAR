﻿using System.Collections;
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
using System.Threading.Tasks;

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
    [SerializeField] SplineComputer splineForScene1;
    [SerializeField] SplineComputer splineForScene2;

    PlacementIndicator placementIndicator;

    public bool canSpawn = false;
    bool hasInternet = false;

    [SerializeField] GameObject tapToPlaceTxt;
    [SerializeField] ARPlaneManager aRPlaneManager;
    public GameObject mainCharacterForScene1, mainCharacterForScene2;
    public LookAtCamera lookCharScene1, lookCharScene2;
    [SerializeField] GameObject spawnVFXForScene1;
    [SerializeField] GameObject planeVFXForScene1;
    [SerializeField] GameObject noInternetPanel;
    public StateHandler stateHandler;
    public List<ToSpawnObject> toSpawnObjects;
    Vector3 spawnPoint;
    Quaternion spawnRot;
    float initialScaleOfScene;

    [Header("SFX")]
    AudioSource audioSource;
    public AudioClip spawnRingClip;
    public AudioClip spawnClip, popClip;
    public void SetSpawnPoint(Vector3 pos, Quaternion rot)
    {
        spawnPoint = pos;
        spawnRot = rot;

        objectToSpawn.transform.position = spawnPoint;
        objectToSpawn.transform.rotation = spawnRot;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        placementIndicator = FindObjectOfType<PlacementIndicator>();

        StartCoroutine(LookForInternetConnection());

        initialScaleOfScene = objectToSpawn.transform.localScale.x;
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
        
        LeanTween.scale(objectToSpawn, Vector3.one * 0.001f, 0.5f).setEase(LeanTweenType.easeInElastic).setOnComplete(() =>
        {
            //audioSource.Play();
            audioSource.PlayOneShot(spawnRingClip);

            var tmpPlaneVFX = Instantiate(planeVFXForScene1, objectToSpawn.transform.position, Quaternion.identity);
            tmpPlaneVFX.transform.localScale = Vector3.zero;

            LeanTween.scale(tmpPlaneVFX, initialScaleOfScene * 1.5f * Vector3.one, 0.7f).setEase(LeanTweenType.easeOutElastic).setOnComplete(() =>
            {
                LeanTween.value(gameObject, 0, 1, 1f).setOnComplete(() =>
                {
                    LeanTween.scale(tmpPlaneVFX, Vector3.one * 1.5f, 2f).setEase(LeanTweenType.easeOutElastic).setOnComplete(() =>
                    {
                        LeanTween.value(gameObject, 0, 1, 1f).setOnComplete(() =>
                        {
                            tmpPlaneVFX.GetComponent<ParticleSystem>().Stop();

                            GameObject particle = Instantiate(spawnVFXForScene1, objectToSpawn.transform.position, Quaternion.Euler(90, 0, 0));
                            particle.transform.localScale /= initialScaleOfScene;

                            audioSource.PlayOneShot(spawnClip);

                            Destroy(particle, 5f);

                            LeanTween.scale(objectToSpawn, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutElastic).setOnComplete(() =>
                            {
                                LeanTween.value(gameObject, 0, 1, 1f).setOnComplete(() =>
                                {
                                    StartCoroutine(SpawnWithDelay(initialScaleOfScene));
                                });
                            });
                        });
                    });
                });
            });
        });

        tapToPlaceTxt.SetActive(false);
    }

    IEnumerator SpawnWithDelay(float initialScaleOfScene)
    {
        float waitTime;

        for (int k = 0; k < toSpawnObjects.Count; k++)
        {
            toSpawnObjects[k].objectsToSpawn.SetActive(true);

            audioSource.PlayOneShot(popClip);
            
            if (toSpawnObjects[k].objectsToSpawn.transform.childCount > 0)
            {
                
                for (int i = 0; i < toSpawnObjects[k].objectsToSpawn.transform.childCount; i++)
                {
                    toSpawnObjects[k].objectsToSpawn.transform.GetChild(i).gameObject.SetActive(true);

                    GameObject tmpParticle = Instantiate(toSpawnObjects[k].VFX, toSpawnObjects[k].objectsToSpawn.transform.GetChild(i).transform.position, Quaternion.Euler(90, 0, 0));
                    tmpParticle.transform.localScale = toSpawnObjects[k].VFXScale / initialScaleOfScene;
                    Destroy(tmpParticle, 5f);


                    yield return null;
                }
            }
            else
            {
                GameObject tmpParticle = Instantiate(toSpawnObjects[k].VFX, toSpawnObjects[k].objectsToSpawn.transform.position, Quaternion.Euler(90, 0, 0));
                tmpParticle.transform.localScale = toSpawnObjects[k].VFXScale / initialScaleOfScene;
                Destroy(tmpParticle, 5f);

            }

            waitTime = ((float)(toSpawnObjects.Count - k) / toSpawnObjects.Count) * 0.2f;

            yield return new WaitForSeconds(waitTime);

        }

        //audioSource.PlayOneShot(birdsChrimpClip);
        audioSource.Play();

        mainCharacterForScene1.SetActive(true);
        var spline = Instantiate(splineForScene1, objectToSpawn.transform);
        var sf = mainCharacterForScene1.GetComponent<SplineFollower>();
        sf.spline = spline;
        sf.enabled = true;
        sf.follow = true;
        sf.SetDistance(0);

        Action<double> myHandler = (o) => 
        {
            StartCoroutine(stateHandler.AppearButtons(0));
            sf.follow = false;

        };

        sf.onEndReached += myHandler;
    }

    public void Spawn2ndScene()
    {
        //text off, record off
        stateHandler.DisappearButtons();

        GameObject particle1 = Instantiate(spawnVFXForScene1, objectToSpawn.transform.position, Quaternion.Euler(90, 0, 0));
        particle1.transform.localScale /= initialScaleOfScene;
        GameObject particle2 = Instantiate(spawnVFXForScene1, objectToSpawn.transform.position, Quaternion.Euler(90, 0, 0));
        particle2.transform.localScale /= initialScaleOfScene;
        GameObject particle3 = Instantiate(spawnVFXForScene1, objectToSpawn.transform.position, Quaternion.Euler(90, 0, 0));
        particle3.transform.localScale /= initialScaleOfScene;
        Destroy(particle1, 5f);
        Destroy(particle2, 5f);
        Destroy(particle3, 5f);

        audioSource.PlayOneShot(spawnClip);

        objectToSpawn.transform.GetChild(0).gameObject.SetActive(false);
        objectToSpawn.transform.GetChild(1).gameObject.SetActive(true);

        //setup splines
        mainCharacterForScene2.transform.GetChild(0).GetComponent<Animator>().Play("G_Normal_Walk");
        var spline = Instantiate(splineForScene2, objectToSpawn.transform.GetChild(1).transform);
        var sf = mainCharacterForScene2.GetComponent<SplineFollower>();
        sf.spline = spline;
        sf.enabled = true;
        sf.follow = true;
        sf.SetDistance(0);
        sf.onEndReached += (o) =>
        {
            stateHandler.PlayS2C1();
            sf.follow = false;
        };
    }
}
    
