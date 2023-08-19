using System;
using System.Collections;
using UnityEngine;

public class MergeSystem : ObjectPool
{
    [SerializeField] private GameObject _mergeEffect;
    [SerializeField] private int _countMergeEffect = 21;
    [SerializeField] private float _animationTime = 3f;
    
    private const float DecelerationFactor = 0.5f;
    private const float MergeTime = 1f;
    private ParticleSystem _tempParticle;

    public event Action<ActiveItem> Spawned;

    public void Initialize()
    {
        for (int i = 0; i < _countMergeEffect; i++)
        {
            Initialize(_mergeEffect);
        }
    }

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

        StartCoroutine(MergeEffectAnimation(toItem.transform.position, _animationTime));
        StartCoroutine(MergeProcess(fromItem, toItem));
    }

    private IEnumerator MergeProcess(ActiveItem fromItem, ActiveItem toItem)
    {
        Vector3 startPosition = fromItem.transform.position;

        for (float timer = 0; timer < MergeTime; timer += Time.deltaTime / DecelerationFactor)
        {
            fromItem.transform.position = Vector3.Lerp(startPosition, toItem.transform.position, timer);
            yield return null;
        }

        fromItem.transform.position = toItem.transform.position;
        fromItem.Merged -= this.Collapse;
        fromItem.gameObject.SetActive(false);
        Spawned?.Invoke(toItem);
        toItem.Merged -= this.Collapse;
    }

    private IEnumerator MergeEffectAnimation(Vector3 position, float animationTime)
    {
        if (TryGetObjectFromPool(out GameObject result))
        {
            result.transform.position = position;
            result.SetActive(true);
        }
        
        for (float timer = 0; timer < animationTime; timer += Time.deltaTime)
        {
            yield return null;
        }
        
        result.SetActive(false);
    }
}
