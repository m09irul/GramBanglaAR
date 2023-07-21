using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRotate : MonoBehaviour
{
    public float rotationSpeed = 0.4f;
    //Drag the camera object here
    public Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        // get the user touch input
        foreach (Touch touch in Input.touches)
        {
            Ray camRay = cam.ScreenPointToRay(touch.position);
            RaycastHit raycastHit;
            if (Physics.Raycast(camRay, out raycastHit, 10))
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.Rotate(touch.deltaPosition.y * rotationSpeed,
                        -touch.deltaPosition.x * rotationSpeed, 0, Space.World);
                }
            }
        }
    }
}
