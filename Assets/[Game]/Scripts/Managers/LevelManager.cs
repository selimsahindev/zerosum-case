using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SelimSahinUtils;

public class LevelManager : MonoBehaviour
{
    [Tooltip("-1 means levels will be loaded in normal order.")]
    [SerializeField] private int playLevel = -1;

    public Level level { get; private set; }

    private int currencyCollected = 0;

    #region Singleton
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        Construct();
    }
    #endregion

    private void Construct()
    {
        int levelIndex = DataManager.Instance.Level;

        if (playLevel != -1)
        {
            levelIndex = playLevel;
        }

        level = Instantiate(Resources.Load<Level>($"Levels/Level-{levelIndex}"));

        EventManager.AddListener(EventNames.OnCollectableInteraction, data => HandleCollectableInteraction((Collectable)data));
        EventManager.AddListener(EventNames.OnGameOver, data => HandleGameOverEvent((bool)data));
    }

    public bool ReloadScene()
    {
        DG.Tweening.DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        return true;
    }

    private void HandleCollectableInteraction(Collectable collectable)
    {
        if (collectable.Type == Collectable.CollectableType.Currency)
        {
            currencyCollected += collectable.Value;
        }
    }

    private void HandleGameOverEvent(bool success)
    {
        // Save collected currencies if the game is completed successfully.
        if (success)
        {
            DataManager.Instance.SetCurrency(DataManager.Instance.Currency + currencyCollected);
        }
    }
}
