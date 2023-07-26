using System;
using UnityEngine;
public class DragObject: ActiveItem
{
    private Plane _dragPlane;
    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
        _dragPlane = new Plane(Vector3.up, Vector3.zero);
    }

    private void OnMouseDown()
    {

    }
 
    private void OnMouseDrag()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        _dragPlane.Raycast(ray, out float distance);
        Vector3 mousePosition = ray.GetPoint(distance);
        transform.position = new Vector3(mousePosition.x, Constants.DragObject.OffsetY, mousePosition.z);
    }

    private void OnMouseUp()
    {
        
    }
}
