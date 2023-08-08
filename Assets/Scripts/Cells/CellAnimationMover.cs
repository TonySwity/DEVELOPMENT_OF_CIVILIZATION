using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CellAnimationMover : MonoBehaviour
{
    [SerializeField] private Transform[] _cellsGroup = {};
    [SerializeField] private Vector3 _startPosition = new Vector3(0f, 0f, 20f); 
    
    public void Initialize()
    {
        StartCoroutine(PlayStartAnimation());
    }
    
    private void PlayStartAnimation1()
    {
        float duration = 2f;
        
        for (int i = 0; i < _cellsGroup.Length; i++)
        {
            _cellsGroup[i].DOMove(Vector3.zero, duration).From(_startPosition);
        }
    }

    public void OpenAge()
    {
        float duration = 1f;
        float firstDOMoveY = -3f;
        float secondDOMoveY = 0f;
        Vector3 firstEndRotate = new Vector3(0, 0, 180);
        Vector3 secondEndRotate = new Vector3(0, 0, -0);
        Sequence sequence = DOTween.Sequence();
        
        for (int i = 0; i < _cellsGroup.Length; i++)
        {
            sequence.Append(_cellsGroup[i].DOMoveY(firstDOMoveY, duration));
            sequence.Join(_cellsGroup[i].DORotate(firstEndRotate, duration));
            sequence.Append(_cellsGroup[i].DOMoveY(secondDOMoveY, duration));
            sequence.Join(_cellsGroup[i].DORotate(secondEndRotate, duration));
        }
    }

    private IEnumerator PlayStartAnimation()
    {
        float duration = 2f;
        float _waitTime = 0.5f;
        
        for (int i = 0; i < _cellsGroup.Length; i++)
        {
            _cellsGroup[i].DOMove(Vector3.zero, duration).From(_startPosition);
            yield return new WaitForSeconds(_waitTime);
        }
    }
}