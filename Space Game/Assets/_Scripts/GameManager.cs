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

    public delegate void OnEndGame();
    public OnEndGame onEndGame;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeOnEndGame();
        InitializeOnTakeDamage();
    }

    private void InitializeOnTakeDamage()
    {
        RPlayer.RPlayerEntity.OnTakeDamage += RUIManager.UpdateLives;
    }

    private void InitializeOnEndGame()
    {
       onEndGame += RPlayer.SavePlayerData;
    }
    private void OnApplicationQuit()
    {
        onEndGame.Invoke();
    }

    public IEnumerator Sleep(float seconds)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1;
    }
}
