using UnityEngine;

public class SelectableObjectManager : MonoBehaviour
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

        if (Input.GetMouseButtonDown(0))
        {
            if (_currentSelectObject)
            {
                _startPosition = _currentSelectObject.transform.position;
            }
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
        
        if (Input.GetMouseButtonUp(0))
        {
            ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitCell;
            float maxDistanceRay = 1000f;

            if (Physics.Raycast(ray, out hitCell, maxDistanceRay, _layerMaskCell))
            {
                if (hitCell.collider.TryGetComponent(out Cell currentCell))
                {
                    if (_currentSelectObject)
                    {
                        _currentSelectObject.transform.position = currentCell.transform.position;
                    }
                }
            }
            else
            {
                if (_currentSelectObject)
                {
                    _currentSelectObject.transform.position = _startPosition;
                }
            }
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
