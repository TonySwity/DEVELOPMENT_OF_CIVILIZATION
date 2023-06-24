using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsClear { get; private set; } = true;
    
    public void MakeBusy()
    {
        IsClear = false;
    }

    public void MakeClear()
    {
        IsClear = true;
    }
}
