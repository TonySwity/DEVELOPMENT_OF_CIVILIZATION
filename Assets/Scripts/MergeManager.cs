using System.Collections;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    private float _decelerationFactor = 0.1f;
    private float _minDistance = 0.2f;

    public void Collapse(ActiveItem fromItem, ActiveItem toItem)
    {
        if (Mathf.Abs(fromItem.transform.position.y - toItem.transform.position.y) <= _minDistance)
        {
            StartCoroutine(MergeProcess(fromItem, toItem));
        }
    }
    
    private IEnumerator MergeProcess(ActiveItem fromItem, ActiveItem toItem)
    {
        if (fromItem.CurrentItemType == toItem.CurrentItemType)
        {
            Vector3 startPosition = fromItem.transform.position;

            for (float t = 0; t < 1f; t += Time.deltaTime / _decelerationFactor)
            {
                fromItem.transform.position = Vector3.Lerp(startPosition, toItem.transform.position, t);
                yield return null;
            }

            fromItem.transform.position = toItem.transform.position;
        }
    }
}
