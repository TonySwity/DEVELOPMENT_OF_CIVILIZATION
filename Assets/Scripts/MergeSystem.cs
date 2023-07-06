using System.Collections;
using UnityEngine;

public class MergeSystem : MonoBehaviour
{
    private const float AccelerationFactor = 0.1f;
    private const float MergeTime = 1f;
    private Spawner _spawner;

    public void Init(Spawner spawner) => _spawner = spawner;

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

            for (float t = 0; t < MergeTime; t += Time.deltaTime / AccelerationFactor)
            {
                fromItem.transform.position = Vector3.Lerp(startPosition, toItem.transform.position, t);
                yield return null;
            }
            
            fromItem.ResetItemTypeCell();
            fromItem.transform.position = toItem.transform.position;
            fromItem.gameObject.SetActive(false);
            toItem.gameObject.SetActive(false);
            _spawner?.SpawnNextActiveItem(toItem);
        }
    }
}
