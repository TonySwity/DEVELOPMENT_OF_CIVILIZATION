using UnityEngine;
using UnityEngine.UI;

public class PausePanelButton : MonoBehaviour
{
    [SerializeField]private PausePanel _pausePanel;
    // Start is called before the first frame update
    private Button _pausePanelButton;
    
    private void Start()
    {
        gameObject.AddComponent<Button>();
        _pausePanelButton = GetComponent<Button>();
        _pausePanelButton.onClick.AddListener(Deactivate) ;
    }

    private void Deactivate(){
        _pausePanel?.gameObject.SetActive(true);
    }

    private void OnDestroy(){
        _pausePanelButton?.onClick.RemoveAllListeners();
    }

}
