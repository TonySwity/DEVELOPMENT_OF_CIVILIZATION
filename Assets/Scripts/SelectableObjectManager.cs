using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum SelectionState
{
    UnitsSelected,
    Frame,
    Other
}

public class SelectableObjectManager : MonoBehaviour
{
    [SerializeField] private SelectableObject _currentSelectObject;

    [SerializeField] private Camera _camera;
    
    private Plane _plane;

    private void Start()
    {
        _camera = Camera.main;
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.cyan);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out SelectableObject hitSelectableObject))
            {

                if (_currentSelectObject)
                {
                    if (_currentSelectObject != hitSelectableObject)
                    {
                        _currentSelectObject.OnUnhover();
                        _currentSelectObject = hitSelectableObject;
                        _currentSelectObject.OnHover();
                    }
                }
                else
                {
                    _currentSelectObject = hitSelectableObject;
                    _currentSelectObject.OnHover();
                }
            }
            else
            {
                UnhoveredCurrent();
            }
            
        }
        else
        {
            UnhoveredCurrent();
        }

        
        if (Input.GetMouseButton(0))
        {
            if (_currentSelectObject)
            {
                ray = _camera.ScreenPointToRay(Input.mousePosition);

                float distance;
                _plane.Raycast(ray, out distance);
                Vector3 mousePosition = ray.GetPoint(distance);
                _currentSelectObject.transform.position = mousePosition;
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            
        }

        if (Input.GetMouseButton(0))
        {
            
        }
                

    }



    private void UnhoveredCurrent()
    {
        if (_currentSelectObject)
        {
            _currentSelectObject.OnUnhover();
            _currentSelectObject = null;
        }
    }
}
