using System;
using System.Collections;
using System.Collections.Generic;
using ModularMotion;
using UnityEngine;
using UnityEngine.InputSystem;
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

        public void OnReload(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            ModularMotion.Core.Motion.currentMotions.Clear();
            ReloadScene();
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
