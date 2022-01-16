using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SelimSahinUtils;

public class UIManager : MonoBehaviour
{
    public MainPanel mainPanel;
    public GamePanel gamePanel;
    public EndPanel endPanel;

    #region Singleton

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Construct();
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    private void Construct()
    {
        mainPanel.Active(true);
        gamePanel.Active(false);
        endPanel.Active(false);

        EventManager.AddListener(EventNames.OnGameStart, OnGameStart);
        EventManager.AddListener(EventNames.OnGameOver, data => OnGameOver((bool)data));
    }

    private void OnGameStart()
    {
        mainPanel.ActiveSmooth(false);
        gamePanel.ActiveSmooth(true);
    }

    private void OnGameOver(bool success)
    {
        gamePanel.ActiveSmooth(false);
        endPanel.ActiveSmooth(true);
    }
}
