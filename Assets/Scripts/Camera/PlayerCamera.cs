using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float currentRotation = 0;
}
    /*
    void Update()
    {
        currentRotation += Input.GetAxis("Mouse Y");
        currentRotation = Mathf.Clamp(currentRotation, -60, 60);
        transform.localRotation = Quaternion.AngleAxis(-currentRotation, Vector3.right);
    }

}
    */
