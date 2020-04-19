using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Script References

    public Player RPlayer;
    public InputManager RInputManager;

    public WaveManager RWaveManager;
    public ObjectPooler RObjectPooler;

    public CameraManager RCameraManager;
    public UIManager RUIManager;
    public AudioManager RAudioManager;

    #endregion

    private void Start()
    {
        InitializeOnTakeDamage();
    }

    private void InitializeOnTakeDamage()
    {
        RUIManager.RPlayerEntity = RPlayer.RPlayerEntity;
        RPlayer.RPlayerEntity.OnTakeDamage += RUIManager.TakeDamage;
    }

}
