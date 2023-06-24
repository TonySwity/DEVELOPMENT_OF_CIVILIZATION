using UnityEngine;

public class SelectableObjectManager : MonoBehaviour
{
    [SerializeField] private SelectableObject _currentSelectObject;
    [SerializeField] private LayerMask _layerMaskCell;
    [SerializeField] private LayerMask _layerMaskActiveItem;

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

        FindSelectableObject();

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
            FindClearCell();
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

    private void FindSelectableObject()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) == false)
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
    }

    private void FindClearCell()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitCell;
        float maxDistanceRay = 1000f;

        int activeItemCount = 0;
        Collider[] hitColliders = {};
        int maxActiveItem = 1;
        float _radiusSphere = 0.5f;

        if (Physics.Raycast(ray, out hitCell, maxDistanceRay, _layerMaskCell))
        {

            if (hitCell.collider.TryGetComponent(out Cell currentCell) == false)
            {
                return;
            }

            hitColliders = Physics.OverlapSphere(currentCell.transform.position, _radiusSphere);

            if (_currentSelectObject)
            {
                _currentSelectObject.transform.position = currentCell.transform.position;
            }
        }
        else
        {
            if (_currentSelectObject)
            {
                _currentSelectObject.transform.position = _startPosition;
            }
        }

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out ActiveItem activeItem))
            {
                activeItemCount++;
            }
        }

        if (activeItemCount <= maxActiveItem)
        {
            return;
        }
        Debug.Log(activeItemCount);
        _currentSelectObject.transform.position = _startPosition;
    }
}
