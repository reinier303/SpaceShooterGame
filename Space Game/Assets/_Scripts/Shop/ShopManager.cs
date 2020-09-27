using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public PlayerData Data;

    public Weapon Bullets;

    private void Awake()
    {
        Time.timeScale = 1;
        Instance = this;

        Data = SaveLoad.Load<PlayerData>("PlayerData.sav");
    }

    public void RemoveUnits(float Units)
    {
        Data.TotalUnits -= Units;
    }

    public float GetUnits()
    {
        return Data.TotalUnits;
    }

    //TODO:Loading screen and helper loadscene method instead of here
    public void LoadSceneAsync(int scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
