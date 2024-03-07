using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CellAnimationMover : MonoBehaviour
{
    [SerializeField] private Transform[] _cellsGroup = {};
    [SerializeField] private Vector3 _startPosition = new Vector3(0f, 0f, 20f); 
    
    private Vector3 _endPosition = Vector3.zero;
    private Vector3 _firstEndRotate = new Vector3(0, 0, 180);
    private Vector3 _secondEndRotate = new Vector3(0, 0, -0);
    
    public void Initialize()
    {
        StartCoroutine(PlayStartAnimation());
    }
    public void OpenAge()
    {
        float duration = 1f;
        float firstDOMoveY = -3f;
        float secondDOMoveY = 0f;

        Sequence sequence = DOTween.Sequence();
        
        for (int i = 0; i < _cellsGroup.Length; i++)
        {
            sequence.Append(_cellsGroup[i].DOMoveY(firstDOMoveY, duration));
            sequence.Join(_cellsGroup[i].DORotate(_firstEndRotate, duration));
            sequence.Append(_cellsGroup[i].DOMoveY(secondDOMoveY, duration));
            sequence.Join(_cellsGroup[i].DORotate(_secondEndRotate, duration));
        }
    }

    private IEnumerator PlayStartAnimation()
    {
        float duration = 5f;
        float step = 0.3f;
        float _waitTime = 0.5f;
        
        
        for (int i = 0; i < _cellsGroup.Length; i++)
        {
            _cellsGroup[i].DOMove(_endPosition, duration).From(_startPosition);
            _endPosition = new Vector3(_endPosition.x, _endPosition.y, _endPosition.z + step);
            yield return new WaitForSeconds(_waitTime);
        }
    }
}