using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Script References

    public Player RPlayer;
    private InputManager RInputManager;

    private WaveManager RWaveManager;
    private ObjectPooler RObjectPooler;

    private CameraManager RCameraManager;
    private UIManager RUIManager;
    private AudioManager RAudioManager;

    #endregion

    private void Start()
    {
        InitializeOnTakeDamage();
    }

    private void InitializeOnTakeDamage()
    {
        //RPlayer.RPlayerEntity.OnTakeDamage += RPlayer.RPlayerEntity.TakeDamage;
    }

}
