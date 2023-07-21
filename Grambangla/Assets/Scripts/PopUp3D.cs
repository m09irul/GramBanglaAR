using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp3D : MonoBehaviour
{
    public GameObject bigVersion;

    private void OnMouseDown()
    {
        bigVersion.SetActive(true);
    }
}
