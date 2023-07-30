using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(SphereCollider))]
public class Cell : MonoBehaviour, ICellable
{
    [field: SerializeField] public ItemType CurrentItemType { get; private set; }

    private MeshRenderer _meshRenderer;
    private SphereCollider _sphereCollider;
    
    public event Action<ItemType> Achieved;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _sphereCollider = GetComponent<SphereCollider>();
    }


    public void SetCurrentItemType(ItemType itemType)
    {
        CurrentItemType = itemType;
    }

    public void EnableCollider() => _sphereCollider.enabled = true;

    public void DisableCollider() => _sphereCollider.enabled = false;

    public void SetColorMaterial(Material material) => _meshRenderer.material = material;
}

