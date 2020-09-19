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
    public Slider WeaponExperience;

    [Header("BounceTweenValues")]
    public float BounceTime;
    public float BounceSize;
    public LeanTweenType BounceType;

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
}
