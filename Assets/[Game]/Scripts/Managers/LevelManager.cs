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

    // This can be stored in a more specific area like "Resource Manager". But for the sake of simplicity I wrote it here.
    [HideInInspector] public ParticleSystem coinSplashPrefab;

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
        coinSplashPrefab = Resources.Load<ParticleSystem>("Particles/VFX_Coins_Splash");

        // This line contains additional operations for the level loop
        int levelIndex = (DataManager.Instance.Level % Resources.LoadAll<Level>("Levels").Length) + 1;

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
        UIManager.Instance.fadePanel.ActiveSmooth(true, 0.25f, onComplete: () => {
            DG.Tweening.DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

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
            DataManager.Instance.SetLevel(DataManager.Instance.Level + 1);
            DataManager.Instance.SetCurrency(DataManager.Instance.Currency + currencyCollected);
        }
    }
}
