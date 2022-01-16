using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using SelimSahinUtils;

public class MainPanel : Panel
{
    [SerializeField] private Button tapToPlayButton;
    [SerializeField] private Button upgradeStackButton;
    [SerializeField] private TextMeshProUGUI tapToPlayText;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI upgradeStackPriceText;
    [SerializeField] private TextMeshProUGUI currentStackUpgradeText;

    private void Awake()
    {
        tapToPlayButton.onClick.AddListener(OnTapToPlayButtonClicked);
        upgradeStackButton.onClick.AddListener(OnUpgradeStackButtonClicked);

        tapToPlayText.transform.DOScale(1.05f, 1f).SetLoops(-1, LoopType.Yoyo);

        SetCurrentLevelText(DataManager.Instance.Level);
        SetCurrencyText(DataManager.Instance.Currency);
        SetCurrentStackUpgradeText(DataManager.Instance.StackUpgrade);
        SetStackUpgradePriceText(GetUpgradePrice(DataManager.Instance.StackUpgrade + 1));

        if (!IsStackUpgradeAvailable())
        {
            upgradeStackButton.interactable = false;
        }
    }

    private void OnTapToPlayButtonClicked()
    {
        EventManager.NotifyListeners(EventNames.OnGameStart);
        tapToPlayButton.interactable = false;
    }

    private void OnUpgradeStackButtonClicked()
    {
        int currentUpgrade = DataManager.Instance.StackUpgrade;
        int remainingCurrency = DataManager.Instance.Currency - GetUpgradePrice(currentUpgrade + 1);

        SetCurrencyText(remainingCurrency);
        DataManager.Instance.SetCurrency(remainingCurrency);

        SetCurrentStackUpgradeText(currentUpgrade + 1);
        DataManager.Instance.SetStackUpgrade(currentUpgrade + 1);

        SetStackUpgradePriceText(GetUpgradePrice(currentUpgrade + 2));

        if (!IsStackUpgradeAvailable())
        {
            upgradeStackButton.interactable = false;
        }
    }

    private bool IsStackUpgradeAvailable()
    {
        return DataManager.Instance.Currency >= GetUpgradePrice(DataManager.Instance.StackUpgrade + 1);
    }

    private void SetCurrentLevelText(int level)
    {
        currentLevelText.text = $"Level {level}";
    }

    private void SetCurrencyText(int amount)
    {
        coinText.text = amount.ToString();
    }

    private int GetUpgradePrice(int upgradeLevel)
    {
        int basePrice = 50;
        return (int)(basePrice * Mathf.Pow(upgradeLevel, 1.2f));
    }

    private void SetStackUpgradePriceText(int price)
    {
        upgradeStackPriceText.text = price.ToString();
    }

    private void SetCurrentStackUpgradeText(int current)
    {
        currentStackUpgradeText.text = $"({current})";
    }
}
