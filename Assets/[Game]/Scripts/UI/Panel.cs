using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public abstract class Panel : MonoBehaviour
{
    public CanvasGroup group {
        get {
            return GetComponent<CanvasGroup>();
        }
    }

    public void Active(bool isActive)
    {
        group.alpha = isActive ? 1 : 0;
        gameObject.SetActive(isActive);
    }

    public void ActiveSmooth(bool isActive, float duration = 0.5f, UnityAction onComplete = null)
    {
        if (isActive)
        {
            gameObject.SetActive(true);
            group.DOFade(1f, duration).OnComplete(() => onComplete?.Invoke());
        }
        else
        {
            group.DOFade(0f, duration).OnComplete(() => {
                gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        }
    }
}