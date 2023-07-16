using UnityEngine;
using DG.Tweening;

public class CellAnimationMover : MonoBehaviour
{
    [SerializeField] private Transform[] _cellsGroup = {};
    
    private void Start()
    {
        //PlayStartAnimation();
        OpenIronAge();
    }
    
    private void PlayStartAnimation()
    {
        //вернуть на 10 и -10
        float duration = 2f;
        
        for (int i = 0; i < _cellsGroup.Length; i++)
        {
            _cellsGroup[i].DOMove(Vector3.zero, duration);
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
    
    
    //необходимо реализовать Когда появиться новый интересный айтем камера вокруг перса
}