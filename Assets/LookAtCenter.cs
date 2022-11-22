using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCenter : MonoBehaviour
{
    void Update()
    {
        RaycastHit screenRayInfo;
        Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out screenRayInfo, Mathf.Infinity);
        transform.LookAt(screenRayInfo.point);
    }
}
