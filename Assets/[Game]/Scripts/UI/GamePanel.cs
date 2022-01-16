using UnityEngine;
using TMPro;
using DG.Tweening;
using SelimSahinUtils;

public class GamePanel : Panel
{
    [SerializeField] private TextMeshProUGUI coinText;

    private Vector3 initialScale;
    private int initialCurrency;

    private void Awake()
    {
        initialScale = transform.localScale;
        initialCurrency = DataManager.Instance.Currency;

        SetCurrencyText(initialCurrency);

        EventManager.AddListener(EventNames.OnCollectableInteraction, data => HandleCollectableInteraction((Collectable)data));
    }

    private void HandleCollectableInteraction(Collectable collectable)
    {
        if (collectable.Type == Collectable.CollectableType.Currency)
        {
            initialCurrency += collectable.Value;
            SetCurrencyText(initialCurrency);
        }
    }

    private void SetCurrencyText(int amount)
    {
        coinText.DOKill();
        coinText.transform.DOScale(initialScale * 1.1f, 0.2f).OnComplete(() => coinText.transform.DOScale(initialScale, 0.2f));
        coinText.text = amount.ToString();
    }
}
