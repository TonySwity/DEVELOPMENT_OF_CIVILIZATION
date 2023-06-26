using UnityEngine;
//objectmover
public class SelectableObjectMover : MonoBehaviour
{
    [SerializeField] private SelectableObject _currentSelectObject;
    [SerializeField] private LayerMask _layerMaskCell;

    private Camera _camera;
    private Plane _plane;
    private Vector3 _startPosition;

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
            
            if (_currentSelectObject)
            {
                _startPosition = _currentSelectObject.transform.position;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (_currentSelectObject)
            {
                float distance;
                _plane.Raycast(ray, out distance);
                Vector3 mousePosition = ray.GetPoint(distance);
                _currentSelectObject.transform.position = mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            FindClearCell();
            float maxDistanceRay = 500f;
            
            if(Physics.Raycast(ray,out RaycastHit hit, maxDistanceRay,_layerMaskCell))
            {
                if (hit.collider.TryGetComponent(out Cell cell))
                {
                    if (cell.CurrentItemType != ItemType.Empty)
                    {
                        return;
                    }
                    
                    cell.SetCurrentItemType(_currentSelectObject.GetCurrentItemType());
                }
            }
        }
    }

    private void UnhoveredCurrent()
    {
        if (!_currentSelectObject)
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
        float distance = 500f;
        

        if (Physics.Raycast(ray, out hit) == false)
        {
            UnhoveredCurrent();
            return;
        }

        if (hit.collider.TryGetComponent(out SelectableObject selectableObject) == false)
        {
            return;
        }

        if (_currentSelectObject != selectableObject)
        {
            _currentSelectObject?.OnUnhover();
        }

        _currentSelectObject = selectableObject;
        _currentSelectObject.OnHover();
        
        if (Physics.Raycast(ray, out hit, distance, _layerMaskCell))
        {
            if (hit.collider.TryGetComponent(out Cell cell))
            {
                if (cell.CurrentItemType != ItemType.Empty)
                {
                    cell.SetCurrentItemType(ItemType.Empty);
                }
            }
        }
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
            
            if (_currentSelectObject && currentCell.CurrentItemType == ItemType.Empty)
            {
                SetNewPosition(currentCell.transform.position);
            }
            else
            {
                SetNewPosition(_startPosition);
            }
        }
        else
        {
            if (_currentSelectObject)
            {
                SetNewPosition(_startPosition);
            }
        }
    }

    private void SetNewPosition(Vector3 position) => _currentSelectObject.transform.position = position;
}
