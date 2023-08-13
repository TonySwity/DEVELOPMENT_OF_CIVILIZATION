using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialScene : MonoBehaviour
{
    private SceneLoader _sceneLoader = new SceneLoader();

    private void Start()
    {
        StartCoroutine(_sceneLoader.LoadScene(Constants.Scenes.GameplayScene));
    }
}
