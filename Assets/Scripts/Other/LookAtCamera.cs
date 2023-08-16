using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform _cameraTransform;
    
    private void OnEnable()
    {
        _cameraTransform = Camera.main.transform;
        Vector3 toCamera = transform.position - _cameraTransform.position;
        transform.rotation = Quaternion.LookRotation(toCamera);
    }
}
