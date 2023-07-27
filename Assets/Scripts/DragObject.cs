using System;
using UnityEngine;
public class DragObject: ActiveItem
{
    private Plane _dragPlane;
    private Camera _camera;
    private Vector3 _startPosition;
    [SerializeField] private LayerMask _layerMaskCell;
    
    private void Awake()
    {
        _camera = Camera.main;
        _dragPlane = new Plane(Vector3.up, Vector3.zero);
    }

    private void OnMouseDown()
    {
        SetStartPosition();
        
        OnHover();
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Constants.DragObject.MaxDistanceRay, _layerMaskCell) == false)
        {
            return;
        }

        if (hit.collider.TryGetComponent(out Cell cell) == false)
        {
            return;
        }

        if (cell.CurrentItemType == ItemType.Empty)
        {
            return;
        }
        
        cell.SetCurrentItemType(ItemType.Empty);
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
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Constants.DragObject.MaxDistanceRay, _layerMaskCell) == false)
        {
            SetNewPosition(_startPosition);
            OnUnhover();
            return;
        }

        if (hit.collider.TryGetComponent(out Cell cell) == false)
        {
                
            return;
        }

        if (cell.CurrentItemType != ItemType.Empty)
        {
            SetNewPosition(_startPosition);
            OnHover();
            return;
        }
        
        SetNewPosition(cell.transform.position);
        cell.SetCurrentItemType(CurrentItemType);
        OnUnhover();
    }
    
    private void SetNewPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, Constants.DragObject.OffsetY, position.z);
    }

    private void SetStartPosition()
    {
        _startPosition = new Vector3(transform.position.x, Constants.DragObject.OffsetY, transform.position.z);
    }
}
