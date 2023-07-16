using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
   [SerializeField] private Transform _container;
   
   private List<Enemy> _pool = new List<Enemy>();
    
   protected void Initialize(Enemy enemy)
   {
      Enemy spawned = Instantiate(enemy, _container);
      spawned.gameObject.SetActive(false);
      _pool.Add(spawned);
   }

   protected bool TryGetGameObject(AgeItem ageItem, out Enemy resultGameObject)
   {
      resultGameObject = _pool.FirstOrDefault(target => target.gameObject.activeSelf == false && target.AgeItem == ageItem);

      return resultGameObject != null;
   }
}
