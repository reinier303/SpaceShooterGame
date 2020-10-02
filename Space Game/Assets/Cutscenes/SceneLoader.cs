using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int sceneToLoad;
    private void OnEnable()
    {
        //TODO:LoadingScreen
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
