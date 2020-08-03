using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;

        private void Awake()
        {
            if(!SaveLoad.SaveExists("PlayerData"))
            {
                SaveLoad.NewSave();
            }
        }

        public void LoadScene(int scene)
        {
            ExtensionMethods.LoadSceneWithLoadingScreen(scene, loadingScreen, this);
        }
    }
}


