using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using SelimSahinUtils;

public class StackBar : Panel
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Sprite[] diamondSprites;
    public Image diamondImage;

    private Tween fillTween;

    private void Awake()
    {
        fillImage.fillAmount = 0f;

        StartCoroutine(DiamondImageAnimation());

        EventManager.AddListener(EventNames.OnCollectableInteraction, data => HandleCollectableInteraction((Collectable)data));
    }

    private void HandleCollectableInteraction(Collectable collectable)
    {
        if (collectable.Type == Collectable.CollectableType.Stack)
        {
            diamondImage.transform.DOKill();
            diamondImage.transform.DOScale(1.25f, 0.2f).OnComplete(() => diamondImage.transform.DOScale(1f, 0.2f));
        }
    }

    public void SetFillAmount(float fillAmount, float duration = 0.25f)
    {
        fillTween.Kill();
        fillTween = fillImage.DOFillAmount(fillAmount, duration);
    }

    private IEnumerator DiamondImageAnimation()
    {
        int spriteIndex = 0;

        while (true)
        {
            diamondImage.sprite = diamondSprites[spriteIndex];

            yield return new WaitForSeconds(0.25f);

            spriteIndex = ++spriteIndex % diamondSprites.Length;
        }
    }
}
