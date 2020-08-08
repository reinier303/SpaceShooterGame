using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceGame
{
    public static class ExtensionMethods
    {
        public static void LoadSceneWithLoadingScreen(int scene, GameObject loadingScreen, MonoBehaviour runOn)
        {
            runOn.StartCoroutine(LoadSceneIENumerator(scene, loadingScreen));
        }

        public static IEnumerator LoadSceneIENumerator(int scene, GameObject loadingScreen)
        {
            loadingScreen.SetActive(true);

            AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

            while (!operation.isDone)
            {
                yield return null;
            }
        }
    }
}

