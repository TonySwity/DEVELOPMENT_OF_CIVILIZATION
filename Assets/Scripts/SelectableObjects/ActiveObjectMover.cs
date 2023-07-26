using System;
using UnityEngine;

public class ActiveObjectMover : MonoBehaviour
{
    private const float MaxDistanceRay = 500f;
    private const float OffsetY = 0.2f;

    [SerializeField] private LayerMask _layerMaskCell;

    [SerializeField]private ActiveItem _currentActiveObject;
    private Cell _cell;
    private Camera _camera;
    private Plane _plane;
    [SerializeField]private Vector3 _startPosition;

    private void Start()
    {
        _camera = Camera.main;
        _plane = new Plane(Vector3.up, Vector3.zero);
    }
    

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, MaxDistanceRay) == false)
            {
                return;
            }

            if (hit.collider.TryGetComponent(out ActiveItem activeItem) == false)
            {
                return;
            }
            _currentActiveObject = activeItem;
            SetStartPositionCurrentActiveObject();
            _currentActiveObject.OnHover();

            if (Physics.Raycast(ray, out hit, MaxDistanceRay, _layerMaskCell) == false)
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

            _cell = cell;
            _cell.SetCurrentItemType(ItemType.Empty);
        }

        if (Input.GetMouseButton(0))
        {
            // _plane.Raycast(ray, out float distance);
            // Vector3 mousePosition = ray.GetPoint(distance);
            // SetNewPositionCurrentActiveObject(mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_currentActiveObject == null)
            {
                return;
            }
            
            if (Physics.Raycast(ray, out RaycastHit hit, MaxDistanceRay, _layerMaskCell) == false)
            {
                SetNewPositionCurrentActiveObject(_startPosition);
                return;
            }

            if (hit.collider.TryGetComponent(out Cell cell) == false)
            {
                
                return;
            }
            
            _cell = cell;
            SetNewPositionCurrentActiveObject(cell.CurrentItemType == ItemType.Empty ? _cell.transform.position : _startPosition);
            _cell.SetCurrentItemType(_currentActiveObject.CurrentItemType);
            _currentActiveObject.OnUnhover();
            // UnhoveredCurrent();
        }
    }

    private void UnhoveredCurrent()
    {
        if (_currentActiveObject == null)
        {
            return;
        }

        _currentActiveObject.OnUnhover();
        _currentActiveObject = null;
    }

    /*private void FindSelectableObject()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, MaxDistanceRay) == false)
        {
            UnhoveredCurrent();
            return;
        }

        if (hit.collider.TryGetComponent(out SelectableObject selectableObject) == false)
        {
            return;
        }

        UnhoveredCurrent();

        _currentSelectObject = selectableObject;
        _currentSelectObject.OnHover();

        if (Physics.Raycast(ray, out hit, MaxDistanceRay, _layerMaskCell) == false)
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

        _cell = cell;
        _currentSelectObject.ActiveItem.SetCurrentCell(cell);
        cell.SetCurrentItemType(ItemType.Empty);
    }*/

    private void FindClearCell()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitCell;
        float maxDistanceRay = 500f;

        if (Physics.Raycast(ray, out hitCell, maxDistanceRay, _layerMaskCell) == false)
        {
            return;
        }
        
        if (hitCell.collider.TryGetComponent(out Cell currentCell) == false)
        {
                return;
        }
        
        // if (_cell == null && _activeObject == null)
        // {
        //     _cell.SetCurrentItemType(_activeObject.CurrentItemType);
        // }

        _cell = currentCell;
        SetNewPositionCurrentActiveObject(currentCell.CurrentItemType == ItemType.Empty ? currentCell.transform.position : _startPosition);
        _currentActiveObject.OnUnhover();
        // {
        //     if (_cell != null && _activeObject)
        //     {
        //        _cell.SetCurrentItemType(_activeObject.CurrentItemType);
        //     }
        //
        //     SetNewPositionCurrentSelectableObject(_startPosition);
        //     UnhoveredCurrent();
        // }
    }

    private void SetNewPositionCurrentActiveObject(Vector3 position)
    {
        _currentActiveObject.transform.position = new Vector3(position.x, OffsetY, position.z);
        
    }

    private void SetStartPositionCurrentActiveObject()
    {
        _startPosition = new Vector3(_currentActiveObject.transform.position.x, OffsetY, _currentActiveObject.transform.position.z);
    }
}