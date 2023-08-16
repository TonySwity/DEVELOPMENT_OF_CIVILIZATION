using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    
    private List<GameObject> _pool = new List<GameObject>();
    
    protected void Initialize(GameObject gObject)
    {
        GameObject spawned = Instantiate(gObject, _container);
        spawned.SetActive(false);
        _pool.Add(spawned);
    }

    protected bool TryGetObjectFromPool(out GameObject result)
    {
        result = _pool.FirstOrDefault(gObject => gObject.activeSelf == false);
        
        return result != null;
    }
}