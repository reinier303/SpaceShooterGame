using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class LoadingScreen : MonoBehaviour
    {
        public float FadeTime;
        public LeanTweenType TweenType;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            FadeLoadingScren();
        }

        private void FadeLoadingScren()
        {
            canvasGroup.alpha = 0;
            LeanTween.alphaCanvas(canvasGroup, 1, FadeTime).setEase(TweenType).setIgnoreTimeScale(true);
        }

    }

}
