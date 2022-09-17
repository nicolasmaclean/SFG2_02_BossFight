using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Utils
{
    public class SceneController : MonoBehaviour
    {
        public static int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;
        public void Quit()
        {
            Application.Quit();
            #if UNITY_EDITOR
            print("Quitting Game...");
            #endif
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(CurrentSceneIndex);
        }
        
        public void LoadNextScene()
        {
            int nextIndex = CurrentSceneIndex + 1;
            if (nextIndex >= SceneManager.sceneCountInBuildSettings) return;

            SceneManager.LoadScene(nextIndex);
        }
    }
}
