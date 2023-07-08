using UnityEngine;

public class SelectableObjectMover : MonoBehaviour
{
    private const float MaxDistanceRay = 500f;

    [SerializeField] private LayerMask _layerMaskCell;
    
    private SelectableObject _currentSelectObject;
    private ICellable _cell;
    private Camera _camera;
    private Plane _plane;
    private Vector3 _startPosition;
    private Vector3 _offset = Vector3.up * 0.2f;
    
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
            FindSelectableObject();
            SetStartPositionCurrentSelectableObject();
        }

        if (Input.GetMouseButton(0))
        {
            _plane.Raycast(ray, out float distance);
            Vector3 mousePosition = ray.GetPoint(distance);
            SetNewPositionCurrentSelectableObject(mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            FindClearCell();

            if (_currentSelectObject == null)
            {
                return;
            }

            if (Physics.Raycast(ray, out RaycastHit hit, MaxDistanceRay, _layerMaskCell) == false)
            {
                _cell.SetCurrentItemType(_currentSelectObject.GetCurrentItemType());
                return;
            }

            if (hit.collider.TryGetComponent(out Cell cell) == false)
            {
                return;
            }

            if (cell.CurrentItemType != ItemType.Empty)
            {
                return;
            }

            _currentSelectObject.ActiveItem.SetCurrentCell(cell);
            cell.SetCurrentItemType(_currentSelectObject.GetCurrentItemType());
            UnhoveredCurrent();
        }
    }

    private void UnhoveredCurrent()
    {
        if (_currentSelectObject == null)
        {
            return;
        }

        _currentSelectObject.OnUnhover();
        _currentSelectObject = null;
    }

    private void FindSelectableObject()
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
    }

    private void FindClearCell()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitCell;
        float maxDistanceRay = 500f;
        Collider[] hitColliders = {};

        if (Physics.Raycast(ray, out hitCell, maxDistanceRay, _layerMaskCell))
        {
            if (hitCell.collider.TryGetComponent(out Cell currentCell) == false)
            {
                return;
            }

            _cell = currentCell;
            SetNewPositionCurrentSelectableObject(currentCell.CurrentItemType == ItemType.Empty ? currentCell.transform.position : _startPosition);
        }
        else
        {
            if (_cell != null && _currentSelectObject)
            {
                _cell.SetCurrentItemType(_currentSelectObject.GetCurrentItemType());
            }

            SetNewPositionCurrentSelectableObject(_startPosition);
            UnhoveredCurrent();
        }
    }

    private void SetNewPositionCurrentSelectableObject(Vector3 position)
    {
        if (_currentSelectObject)
        {
            _currentSelectObject.transform.position = position + _offset;
        }
    }

    private void SetStartPositionCurrentSelectableObject()
    {
        if (_currentSelectObject)
        {
            _startPosition = _currentSelectObject.transform.position;
        }
    }
}