using System.Collections;
using UnityEngine;

public class MergeSystem : MonoBehaviour
{
    private const float DecelerationFactor = 0.5f;
    private const float MergeTime = 1f;
    private ActiveItemSpawner _activeItemSpawner;

    public void Init(ActiveItemSpawner activeItemSpawner) => _activeItemSpawner = activeItemSpawner;

    public void Collapse(ActiveItem firstActiveItem, ActiveItem secondActiveItem)
    {
        ActiveItem fromItem;
        ActiveItem toItem;
        
        if (firstActiveItem.ItemID > secondActiveItem.ItemID)
        {
            fromItem = firstActiveItem;
            toItem = secondActiveItem;
        }
        else
        {
            fromItem = secondActiveItem;
            toItem = firstActiveItem;
        }

        StartCoroutine(MergeProcess(fromItem, toItem));
    }
    
    private IEnumerator MergeProcess(ActiveItem fromItem, ActiveItem toItem)
    {
        if (fromItem.CurrentItemType == toItem.CurrentItemType)
        {
            Vector3 startPosition = fromItem.transform.position;

            for (float t = 0; t < MergeTime; t += Time.deltaTime / DecelerationFactor)
            {
                fromItem.transform.position = Vector3.Lerp(startPosition, toItem.transform.position, t);
                yield return null;
            }
            
            fromItem.ResetItemTypeCell();
            fromItem.transform.position = toItem.transform.position;
            fromItem.gameObject.SetActive(false);
            _activeItemSpawner?.SpawnNextActiveItem(toItem);
        }
    }
}
