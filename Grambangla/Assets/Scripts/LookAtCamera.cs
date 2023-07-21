using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public bool doLookAtCamera = false;
    public bool isFirstTime = true;
    private void Start()
    {
        doLookAtCamera = false;
        isFirstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (doLookAtCamera)
        {
            Vector3 targetDirection = Camera.main.transform.position - transform.position;
            targetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
}
