using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARGestureController : MonoBehaviour
{
    // Reference to the ARSessionOrigin
    public ARSessionOrigin arSessionOrigin;

    // Reference to the XR Gesture Interactor
    public XRGestureInteractor gestureInteractor;

    // Reference to the content you want to manipulate
    public Transform content;

    // The speed of rotation
    public float rotationSpeed = 1f;

    // The speed of scaling
    public float scalingSpeed = 1f;

    // The minimum and maximum scale values
    public float minScale = 0.5f;
    public float maxScale = 2f;

    // The current scale value
    private float currentScale = 1f;

    // The current rotation angle
    private float currentAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Register the gesture events
        gestureInteractor.onPinchUpdated.AddListener(OnPinchUpdated);
        gestureInteractor.onDragUpdated.AddListener(OnDragUpdated);
        gestureInteractor.onTwistUpdated.AddListener(OnTwistUpdated);
    }

    // Update is called once per frame
    void Update()
    {
        // Apply the rotation and scale to the content
        content.rotation = Quaternion.AngleAxis(currentAngle, Vector3.up);
        content.localScale = Vector3.one * currentScale;
    }

    // Handle the pinch gesture
    private void OnPinchUpdated(PinchGesture gesture)
    {
        if (gesture.isStarted || gesture.isRunning)
        {
            if (IsTouchingObject(gesture.position)) // Check if it is touching the object area
            {
                // Get the pinch delta and scale it by the scaling speed
                float pinchDelta = gesture.gapDelta * scalingSpeed;

                // Update the current scale value and clamp it between min and max values
                currentScale += pinchDelta;
                currentScale = Mathf.Clamp(currentScale, minScale, maxScale);

                // Make the content appear at the same position as before scaling
                arSessionOrigin.MakeContentAppearAt(content, content.position);
            }
        }
    }

    // Handle the drag gesture
    private void OnDragUpdated(DragGesture gesture)
    {
        if (gesture.isStarted || gesture.isRunning)
        {
            if (gesture.fingerCount == 3) // Check if it is a three finger drag
            {
                if (IsTouchingObject(gesture.position)) // Check if it is touching the object area
                {
                    // Get the drag delta and scale it by the current scale value
                    Vector3 dragDelta = gesture.delta * currentScale;

                    // Update the content position by adding the drag delta
                    content.position += dragDelta;

                    // Make the content appear at the same position as before moving
                    arSessionOrigin.MakeContentAppearAt(content, content.position);
                }
            }
        }
    }

    // Handle the twist gesture
    private void OnTwistUpdated(TwistGesture gesture)
    {
        if (gesture.isStarted || gesture.isRunning)
        {
            if (gesture.fingerCount == 2) // Check if it is a two finger twist
            {
                if (IsTouchingObject(gesture.position)) // Check if it is touching the object area
                {
                    // Get the twist delta and scale it by the rotation speed
                    float twistDelta = gesture.delta * rotationSpeed;

                    // Update the current angle value and wrap it between 0 and 360 degrees
                    currentAngle += twistDelta;
                    currentAngle %= 360f;
                }
            }
        }
    }

    // Check if a screen position is touching the object area using a raycast
    private bool IsTouchingObject(Vector2 screenPosition)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arSessionOrigin.Raycast(screenPosition, hits, TrackableType.All);
        foreach (var hit in hits)
        {
            if (hit.transform == content) return true;
        }
        return false;
    }
}
```