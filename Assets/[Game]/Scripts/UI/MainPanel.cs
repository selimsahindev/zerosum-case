using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using SelimSahinUtils;

public class MainPanel : Panel
{
    [SerializeField] private Button tapToPlayButton;
    [SerializeField] private TextMeshProUGUI tapToPlayText;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        tapToPlayButton.onClick.AddListener(OnTapToPlayButtonClicked);

        tapToPlayText.transform.DOScale(1.05f, 1f).SetLoops(-1, LoopType.Yoyo);

        SetCurrentLevelText(DataManager.Instance.Level);
        SetCoinText(DataManager.Instance.Currency);
    }

    private void OnTapToPlayButtonClicked()
    {
        EventManager.NotifyListeners(EventNames.OnGameStart);
        tapToPlayButton.interactable = false;
    }

    private void SetCurrentLevelText(int level)
    {
        currentLevelText.text = $"Level {level}";
    }

    private void SetCoinText(int amount)
    {
        coinText.text = amount.ToString();
    }
}
