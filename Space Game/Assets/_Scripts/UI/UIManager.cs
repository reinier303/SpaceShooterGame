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
    public TMP_Text WeaponLevelText;
    public TMP_Text WaveEnterText;

    private CanvasGroup WaveEnterCanvasGroup;

    public Slider WeaponExperience;

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
    private PlayerEntity RPlayerEntity;
    private Player RPlayer;

    private void Awake()
    {
        RPlayer = GameManager.Instance.RPlayer;
        RPlayerEntity = RPlayer.RPlayerEntity;
    }
    private void Start()
    {
        LivesText.text = "Lives:" + RPlayerEntity.currentHealth;
        UnitsText.text = "Units:" + RPlayer.Data.Units;
        WaveEnterCanvasGroup = WaveEnterText.GetComponent<CanvasGroup>();
        StartCoroutine(ShowWaveText());
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

    //Damage number popup
    public void ShowPopUp()
    {

    }

    public void UpdateCurrentWeapon(float currentValue)
    {
        WeaponExperience.value = currentValue;
    }

    public void UpdateCurrentWeaponLevel(float level)
    {
        WeaponLevelText.text = "" + level;
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

    public IEnumerator ShowWaveText()
    {
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
