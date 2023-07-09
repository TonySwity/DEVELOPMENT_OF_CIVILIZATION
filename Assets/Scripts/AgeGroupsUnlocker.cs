using UnityEngine;

public class AgeGroupsUnlocker : MonoBehaviour
{
    [SerializeField] private AgeCellsUnlocker[] _ages = {};
    
    private ICellable[] _cells = { };

    private void Start()
    {
        _ages = transform.GetComponentsInChildren<AgeCellsUnlocker>();
        _cells = transform.GetComponentsInChildren<ICellable>();

        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].Achieved += CheckAchievement;
        }

        for (int i = 0; i < _ages.Length; i++)
        {
            if (i >=  (int)AgeItem.Iron)
            {
                _ages[i].Block();
            }
        }
    }
    
    private void CheckAchievement(ItemType itemType)
    {
        print($"{itemType}");

        if (itemType == ItemType.Home)
        {
            
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].Achieved -= CheckAchievement;
        }
    }
}
