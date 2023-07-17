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

   protected bool TryGetEnemyObject(AgeItem ageItem, out Enemy resultEnemyObject)
   {
      resultEnemyObject = _pool.FirstOrDefault(e => e.gameObject.activeSelf == false && e.AgeItem == ageItem);

      return resultEnemyObject != null;
   }
}
