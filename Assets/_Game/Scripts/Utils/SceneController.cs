using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Utils
{
    public class SceneController : MonoBehaviour
    {
        public void Quit()
        {
            Application.Quit();
            #if UNITY_EDITOR
            print("Quitting Game...");
            #endif
        }

        public void ReloadScene()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }
}
