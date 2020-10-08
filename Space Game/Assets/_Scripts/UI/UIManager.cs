using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UIComponents")]
    public TMP_Text LivesText;
    public TMP_Text UnitsText;
    public TMP_Text PlayerLevelText;
    public TMP_Text WeaponLevelText;
    public TMP_Text WaveEnterText;

    private CanvasGroup WaveEnterCanvasGroup;

    public Slider WeaponExperience;
    public Slider PlayerExperience;
    public Slider WaveProgressBar;

    public GameObject PostGamePanel;
    public GameObject PauseMenu;

    public Image HitVignette;

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

        WaveEnterCanvasGroup = WaveEnterText.GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        LivesText.text = "Lives:" + RPlayerEntity.currentHealth;
        UnitsText.text = "Units:" + RPlayer.Data.Units;

        InitializeWaveUI();

        WaveProgressBar.maxValue = RWaveManager.GetWave(RWaveManager.currentWave).EnemiesForBossSpawn;

        RPlayer.AddExperience(0);
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

    private IEnumerator BounceSizeTween(GameObject uIElement)
    {
        uIElement.transform.localScale = new Vector2(1,1);
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
        PostGamePanelScript.UpdateSummary();
    }

    public IEnumerator ShowWaveText(string waveName, Color textColor, Material textMaterial)
    {
        WaveEnterText.text = "<size=50>Now entering</size>\n" + waveName;
        WaveEnterText.color = textColor;
        WaveEnterText.material = textMaterial;
        LeanTween.alphaCanvas(WaveEnterCanvasGroup, AlphaToWave, WaveTextDuration / 2).setEase(EaseTypeWave);
        yield return new WaitForSeconds(WaveTextDuration);
        LeanTween.alphaCanvas(WaveEnterCanvasGroup, 0, WaveTextDuration / 2).setEase(EaseTypeWave);
    }

    public IEnumerator TweenAlpha(RectTransform rectTransform , float duration, float alphaTo, float alphaFrom)
    {
        LeanTween.alpha(rectTransform, AlphaTo, duration/2).setEase(EaseType);
        yield return new WaitForSeconds(duration / 2);
        LeanTween.alpha(rectTransform, alphaFrom, duration / 2).setEase(EaseType);
    }
}
