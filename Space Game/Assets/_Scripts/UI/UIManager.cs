﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SpaceGame
{
    public class UIManager : MonoBehaviour
    {
        [Header("UIComponents")]
        public TMP_Text LivesText;
        public TMP_Text UnitsText;
        public TMP_Text PlayerLevelText;
        public TMP_Text WeaponLevelText;
        public TMP_Text WaveEnterText;
        public TMP_Text BossEnterText;

        private CanvasGroup waveEnterCanvasGroup;
        private CanvasGroup bossEnterCanvasGroup;

        public Slider WeaponExperience;
        public Slider PlayerExperience;
        public Slider WaveProgressBar;

        public GameObject PostGamePanel;
        public GameObject PauseMenu;

        public Image HitVignette;
        public Image BossIcon;

        [Header("BounceTweenValues")]
        public float BounceTime;
        public float BounceSize;
        public LeanTweenType BounceType;

        [Header("On Hit Vignette Values")]
        public float VignetteDuration;
        public float AlphaTo;
        public LeanTweenType EaseType;

        [Header("Wave Text Values")]
        public float WaveTextDuration;
        public float AlphaToWave;
        public LeanTweenType EaseTypeWave;

        [Header("Script References")]

        public PostGamePanel PostGamePanelScript;

        private GameManager gameManager;
        private WaveManager RWaveManager;
        private PlayerEntity RPlayerEntity;
        private Player RPlayer;

        private void Awake()
        {
            gameManager = GameManager.Instance;
            RWaveManager = gameManager.RWaveManager;
            RPlayer = gameManager.RPlayer;
            RPlayerEntity = RPlayer.RPlayerEntity;

            waveEnterCanvasGroup = WaveEnterText.GetComponent<CanvasGroup>();
            bossEnterCanvasGroup = BossEnterText.GetComponent<CanvasGroup>();
        }
        private void Start()
        {
            LivesText.text = "Lives:" + RPlayerEntity.currentHealth;
            UnitsText.text = "Units:" + RPlayer.Data.Units;

            InitializeWaveUI();

            WaveProgressBar.maxValue = RWaveManager.GetWave(RWaveManager.currentWave).EnemiesForBossSpawn;

            InitializeLevelUI();
        }
        public void InitializeLevelUI()
        {
            RPlayer.AddExperience(0, 0);
            UpdatePlayerLevel(RPlayer.Data.Level);
        }

        public void InitializeWaveUI()
        {
            ScriptableWave currentWave = RWaveManager.GetWave(RWaveManager.currentWave);
            StartCoroutine(ShowWaveText(currentWave.WaveName, currentWave.WaveTextColor, currentWave.WaveTextMaterial));
            WaveProgressBar.maxValue = RWaveManager.GetWave(RWaveManager.currentWave).EnemiesForBossSpawn;
        }

        public void UpdateLives(float damage)
        {
            LivesText.text = "Lives:" + RPlayerEntity.currentHealth;
        }
        public void UpdateUnits(float units)
        {
            StopCoroutine(BounceSizeTween(UnitsText.gameObject));
            StartCoroutine(BounceSizeTween(UnitsText.gameObject));
            UnitsText.text = "Units:" + RPlayer.Data.Units;
        }

        public void UpdateProgressBar()
        {
            WaveProgressBar.value = gameManager.RWaveManager.EnemiesKilledThisWave;
        }

        //Damage number popup
        public void ShowPopUp()
        {

        }

        public void UpdateCurrentWeaponExp(float currentValue)
        {
            WeaponExperience.value = currentValue;
        }

        public void UpdateCurrentWeaponLevel(int level)
        {
            WeaponLevelText.text = "" + level;
        }

        public void UpdatePlayerExp(float currentValue)
        {
            PlayerExperience.value = currentValue;
        }

        public void UpdatePlayerLevel(int level)
        {
            PlayerLevelText.text = "" + level;
        }

        public void UpdateBossHealth(float health)
        {
            WaveProgressBar.value = health;
        }

        private IEnumerator BounceSizeTween(GameObject uIElement)
        {
            uIElement.transform.localScale = new Vector2(1, 1);
            LeanTween.scale(uIElement, new Vector2(BounceSize, BounceSize), BounceTime).setEase(BounceType).setIgnoreTimeScale(true);
            yield return new WaitForSeconds(BounceTime);
            LeanTween.scale(uIElement, new Vector2(1, 1), BounceTime).setEase(BounceType).setIgnoreTimeScale(true);
        }

        public void OpenPauseMenu()
        {
            PauseMenu.SetActive(true);
        }
        public void ClosePauseMenu()
        {
            PauseMenu.SetActive(false);
        }

        public void OnPlayerDeathUI()
        {
            PostGamePanel.SetActive(true);
            StartCoroutine(PostGamePanelScript.UpdateSummary());
        }

        public IEnumerator ShowWaveText(string waveName, Color textColor, Material textMaterial)
        {
            WaveEnterText.text = "<size=50>Now entering</size>\n" + waveName;
            WaveEnterText.color = textColor;
            WaveEnterText.material = textMaterial;
            LeanTween.alphaCanvas(waveEnterCanvasGroup, AlphaToWave, WaveTextDuration / 2).setEase(EaseTypeWave);
            yield return new WaitForSeconds(WaveTextDuration);
            LeanTween.alphaCanvas(waveEnterCanvasGroup, 0, WaveTextDuration / 2).setEase(EaseTypeWave);
        }

        public IEnumerator ShowBossText(string bossText, Color textColor, Material textMaterial)
        {
            BossEnterText.text = bossText;
            BossEnterText.color = textColor;
            BossEnterText.material = textMaterial;
            LeanTween.alphaCanvas(bossEnterCanvasGroup, AlphaToWave, WaveTextDuration / 2).setEase(EaseTypeWave);
            yield return new WaitForSeconds(WaveTextDuration);
            LeanTween.alphaCanvas(bossEnterCanvasGroup, 0, WaveTextDuration / 2).setEase(EaseTypeWave);
        }

        public IEnumerator TweenAlpha(RectTransform rectTransform, float duration, float alphaTo, float alphaFrom)
        {
            LeanTween.alpha(rectTransform, AlphaTo, duration / 2).setEase(EaseType);
            yield return new WaitForSeconds(duration / 2);
            LeanTween.alpha(rectTransform, alphaFrom, duration / 2).setEase(EaseType);
        }

        public void InitializeBossBar(float health, Sprite sprite)
        {
            Debug.Log(health);
            WaveProgressBar.maxValue = health;
            StartCoroutine(LerpBossBar(0, health, 1.5f));
            BossIcon.transform.parent.gameObject.SetActive(true);
            BossIcon.sprite = sprite;
        }

        private IEnumerator LerpBossBar(float StartValue, float EndValue, float LerpTime)
        {
            float StartTime = Time.time;
            float EndTime = StartTime + LerpTime;

            while (Time.time < EndTime)
            {
                float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
                WaveProgressBar.value = Mathf.Lerp(StartValue, EndValue, timeProgressed);

                yield return new WaitForFixedUpdate();
            }
            WaveProgressBar.value = EndValue;
        }

        public void DisableBossBar()
        {
            BossIcon.transform.parent.gameObject.SetActive(false);
        }
    }
}