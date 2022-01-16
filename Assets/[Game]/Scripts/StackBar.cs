using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StackBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private Tween fillTween;

    private void Awake()
    {
        fillImage.fillAmount = 0f;
    }

    public void SetFillAmount(float fillAmount, float duration = 0.25f)
    {
        fillTween.Kill();
        fillTween = fillImage.DOFillAmount(fillAmount, duration);
    }
}
