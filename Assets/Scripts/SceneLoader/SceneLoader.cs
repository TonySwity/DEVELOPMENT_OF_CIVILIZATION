using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader 
{
   public IEnumerator LoadScene(string name, Action onLoaded = null)
   {
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

      while (waitNextScene.isDone == false)
      {
         yield return null;
      }
      
      onLoaded?.Invoke();
   }
}
