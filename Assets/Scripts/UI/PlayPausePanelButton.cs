using UnityEngine;
using UnityEngine.UI;

public class PlayPausePanelButton : MonoBehaviour
{
    [SerializeField]private PausePanel _pausePanel;
     private Button _playPausePanelButton;
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.AddComponent<Button>();
        _playPausePanelButton = GetComponent<Button>();
        _playPausePanelButton.onClick.AddListener(Activate) ;
    }

       private void Activate(){
        _pausePanel?.gameObject.SetActive(false);
    }

     private void OnDestroy(){
        _playPausePanelButton?.onClick.RemoveAllListeners();
    }
}
