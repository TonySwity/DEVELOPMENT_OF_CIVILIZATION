using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ObjectPool : MonoBehaviour
{
   [SerializeField] protected Transform _container;
   
   private List<GameObject> _pool = new List<GameObject>();
    
   protected void Initialize(GameObject gameObj)
   {
      GameObject spawned = Instantiate(gameObject, _container);
      spawned.gameObject.SetActive(false);
      _pool.Add(spawned);
   }

   protected bool TryGetGameObject(out GameObject resultGameObject)
   {
      resultGameObject = _pool.FirstOrDefault(a => a.gameObject.activeSelf == false);

      return resultGameObject != null;
   }
}
