using UnityEngine;

public class BillBoard : MonoBehaviour
{

    public Vector3 offset = new Vector3(0f, 180f, 0f);

    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation *= Quaternion.Euler(offset);
    }
}

