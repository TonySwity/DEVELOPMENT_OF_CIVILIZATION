using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CellAnimationMover : MonoBehaviour
{
    [SerializeField] private Transform[] _cellsGroup = {};

    [SerializeField] private Vector3 _startPosition = new Vector3(0f, 0f, 20f); 
    
    private void Start()
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

    public void OpenIronAge()
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < _cellsGroup.Length; i++)
        {
            sequence.Append(_cellsGroup[i].DOMoveY(-3, 1f));
            sequence.Join(_cellsGroup[i].DORotate(new Vector3(0, 0, 180), 1f));
            sequence.Append(_cellsGroup[i].DOMoveY(0, 1f));
            sequence.Join(_cellsGroup[i].DORotate(new Vector3(0, 0, -0), 1f));
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