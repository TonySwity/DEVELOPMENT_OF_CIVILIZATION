using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveItemPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    private PausePanel _pausePanel;
    
    private List<ActiveItem> _pool = new List<ActiveItem>();
    
    protected void InitializePool(ActiveItem activeItem, Camera gameCamera, PausePanel pausePanel)
    {
        ActiveItem spawned = Instantiate(activeItem, _container);
        spawned.Init(gameCamera);
        _pausePanel = pausePanel;
        _pausePanel.PauseState += spawned.SetActiveAction;
        spawned.gameObject.SetActive(false);
        _pool.Add(spawned);
    }

    protected bool TryGetActiveItemFromPool(ItemType itemType, out ActiveItem activeItemResult)
    {
        activeItemResult = _pool.FirstOrDefault(a => a.gameObject.activeSelf == false && a.CurrentItemType == itemType);

        return activeItemResult != null;
    }

    private void OnDisable()
    {
        foreach (var item in _pool)
        {
            _pausePanel.PauseState -= item.SetActiveAction;
        }
    }
}
