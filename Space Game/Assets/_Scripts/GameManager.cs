using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Script References

    public Player RPlayer;
    public InputManager RInputManager;

    public WaveManager RWaveManager;
    public ObjectPooler RObjectPooler;

    public CameraManager RCameraManager;
    public UIManager RUIManager;
    public AudioManager RAudioManager;

    #endregion

    public System.Action OnEndGame;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeOnTakeDamage();
    }

    private void InitializeOnTakeDamage()
    {
        RUIManager.RPlayerEntity = RPlayer.RPlayerEntity;
        RPlayer.RPlayerEntity.OnTakeDamage += RUIManager.UpdateLives;
    }

    private void InitializeOnEndGame()
    {
        OnEndGame += RPlayer.SavePlayerData;
    }
    private void OnApplicationQuit()
    {
        //OnEndGame.Invoke();
    }
}
