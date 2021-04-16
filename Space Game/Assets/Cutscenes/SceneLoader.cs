using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceGame
{
    public class SceneLoader : MonoBehaviour
    {
        public int SceneToLoad;
        public GameObject LoadingScreen;

        private void OnEnable()
        {
            ExtensionMethods.LoadSceneWithLoadingScreen(SceneToLoad, LoadingScreen, this);
        }
    }

}
