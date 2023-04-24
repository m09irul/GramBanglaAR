using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;

public class ARGestureController : MonoBehaviour
{
    [SerializeField]
    private XROrigin arOrigin;

    [SerializeField]
    private GameObject arObject;

    private float currentDistance;
    private Vector3 initialScale;
    private bool isPinching = false;
    [SerializeField]
    private float minScale = 0.5f;

    [SerializeField]
    private float maxScale = 2f;


    void Update()
    {
        if (Input.touchCount == 2)
        {
            // Get the touches
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Check if at least one finger is touching the AR object
            bool isTouchingObject = false;
            foreach (Touch touch in Input.touches)
            {
                Ray ray = arOrigin.Camera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform.gameObject == arObject)
                {
                    isTouchingObject = true;
                    break;
                }
            }

            if (isTouchingObject)
            {
                // Check if pinch gesture has started
                if (!isPinching)
                {
                    currentDistance = Vector2.Distance(touch1.position, touch2.position);
                    initialScale = arObject.transform.localScale;
                    isPinching = true;
                }
                else
                {
                    // Calculate the new distance and scale the object
                    float newDistance = Vector2.Distance(touch1.position, touch2.position);
                    float scaleFactor = newDistance / currentDistance;
                    scaleFactor = Mathf.Clamp(scaleFactor, minScale / initialScale.x, maxScale / initialScale.x);
                    arObject.transform.localScale = initialScale * scaleFactor;
                }
            }
        }
        else
        {
            isPinching = false;
        }
    }
}