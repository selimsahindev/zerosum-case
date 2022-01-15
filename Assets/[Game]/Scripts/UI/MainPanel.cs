using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using SelimSahinUtils;

public class MainPanel : Panel
{
    [SerializeField] private Button tapToPlayButton;
    [SerializeField] private TextMeshProUGUI tapToPlayText;

    private void Awake()
    {
        tapToPlayButton.onClick.AddListener(OnTapToPlayButtonClicked);

        tapToPlayText.transform.DOScale(1.05f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTapToPlayButtonClicked()
    {
        EventManager.NotifyListeners(EventNames.OnGameStart);
        tapToPlayButton.interactable = false;
    }
}
