using System;
using UnityEngine;

public class ActiveItem : SelectableObject
{
    [SerializeField] private LayerMask _layerMaskActiveItem;
    [SerializeField] private LayerMask _layerMaskCell;

    private Plane _dragPlane;
    private Camera _camera;
    private Vector3 _startPosition;
    private Cell _cell;

    [field: SerializeField]public ItemType CurrentItemType { get; private set; }
    [field: SerializeField]public ItemType NextItem { get; private set; }
    
    public bool IsPaired { get; private set; } = false;
    public int ItemID { get; private set; }
    public bool IsActivateMerge { get; private set; }

    public event Action<ActiveItem, ActiveItem> Merged;
    
    private void Awake()
    {
        _camera = Camera.main;
        _dragPlane = new Plane(Vector3.up, Vector3.zero);
    }
    
    private void OnEnable()
    {
        IsPaired = false;
        
        FindCell();
        
        _cell?.SetCurrentItemType(CurrentItemType);
    }

    public void AddItemID(int itemID) => ItemID = itemID;
    
    public override void OnHover()
    {
        base.OnHover();
        DeactivateMerge();
    }

    public override void OnUnhover()
    {
        base.OnUnhover();
        ActivatedMerge();
        FindActiveItemToMerge();
    }
    
    public void FindActiveItemToMerge()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Constants.ActiveItemMerge.RadiusSphere, _layerMaskActiveItem);
        int minColliders = 2;
        
        if (colliders.Length < minColliders)
        {
            return;
        }
        
        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out ActiveItem activeItem) &&
                CurrentItemType == activeItem.CurrentItemType &&
                ItemID != activeItem.ItemID && IsPaired == false &&
                IsActivateMerge && activeItem.IsActivateMerge)
            {
                IsPaired = true;
                Merged?.Invoke(this, activeItem);
            }
        }
    }
    
    private void ActivatedMerge() => IsActivateMerge = true;
    private void DeactivateMerge() => IsActivateMerge = false;

    protected void Grab()
    {
        SetStartPosition();
        OnHover();
        FindCell();
        _cell?.SetCurrentItemType(ItemType.Empty);
    }

    protected void Drag()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        _dragPlane.Raycast(ray, out float distance);
        Vector3 mousePosition = ray.GetPoint(distance);
        transform.position = new Vector3(mousePosition.x, Constants.DragObject.OffsetY, mousePosition.z);
    }

    protected void LetGo()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Constants.DragObject.MaxDistanceRay, _layerMaskCell) == false)
        {
            SetNewPosition(_startPosition);
            ReturnItemTypeLastCell();
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
            ReturnItemTypeLastCell();
            OnUnhover();
            return;
        }
        
        _cell = cell;
        SetNewPosition(_cell.transform.position);
        _cell.SetCurrentItemType(CurrentItemType);
        OnUnhover();
    }
    
    private void FindCell()
    {
        Ray ray = new Ray(new Vector3(transform.position.x, Constants.ActiveItemMerge.OffsetY, transform.position.z), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, Constants.DragObject.MaxDistanceRay, _layerMaskCell) == false)
        {
            return;
        }

        if (hit.collider.TryGetComponent(out Cell cell) == false)
        {
            return;
        }
        
        _cell = cell;
    }
    
    private void SetNewPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, Constants.DragObject.OffsetY, position.z);
    }

    private void SetStartPosition()
    {
        _startPosition = new Vector3(transform.position.x, Constants.DragObject.OffsetY, transform.position.z);
    }

    private void ReturnItemTypeLastCell()
    {
        _cell.SetCurrentItemType(CurrentItemType);
    }

    private void OnDisable()
    {
        _cell?.SetCurrentItemType(ItemType.Empty);
    }
}
