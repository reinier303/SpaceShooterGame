using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class GameManager : MonoBehaviour
    {
        //Singleton
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

        public bool PlayerAlive;
        public bool BossAlive;
        private bool sleeping;

        public GameObject LoadingScreen;

        //Curren Run Data
        private float runTime;
        [HideInInspector] public float ExperienceEarned;
        [HideInInspector] public int RareDropsFound, EnemiesKilled, BossesKilled;

        private void Awake()
        {
            Time.timeScale = 1;
            Instance = this;
            PlayerAlive = true;
            BossAlive = false;
        }

        private void Start()
        {
            InitializeOnEndGame();
            InitializeOnTakeDamage();
        }

        private void Update()
        {
            runTime += Time.deltaTime;
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
            if (sleeping)
            {
                yield break;
            }

            Time.timeScale = 0;
            sleeping = true;
            yield return new WaitForSecondsRealtime(seconds);
            Time.timeScale = 1;

            //Make sure multiple sleeps cant happen in sequence which caused the game to seem laggy
            yield return new WaitForSeconds(0.2f);
            sleeping = false;
        }

        //TODO:Loading screen and helper loadscene method instead of here
        public void LoadSceneAsync(int scene)
        {
            ExtensionMethods.LoadSceneWithLoadingScreen(scene, LoadingScreen, this);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            RAudioManager.AdjustMusicVolumePaused();
            RUIManager.OpenPauseMenu();
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1;
            RAudioManager.AdjustMusicVolumePaused();
            RUIManager.ClosePauseMenu();
        }

        public void SetSummaryData()
        {
            RUIManager.PostGamePanelScript.RunTimeSeconds = runTime;
            RUIManager.PostGamePanelScript.EnemiesKilled = EnemiesKilled;
            RUIManager.PostGamePanelScript.BossesKilled = BossesKilled;
            RUIManager.PostGamePanelScript.RareDropsFound = RareDropsFound;
            RUIManager.PostGamePanelScript.UnitsEarned = RPlayer.Data.Units;
            RUIManager.PostGamePanelScript.ExperienceEarned = ExperienceEarned;
        }

        public void AddEnemyKilled()
        {
            EnemiesKilled++;
            RWaveManager.EnemyKilled();
        }
    }
}

