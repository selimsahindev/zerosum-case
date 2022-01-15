using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("-1 means levels will be loaded in normal order.")]
    [SerializeField] private int playLevel = -1;

    public Level level { get; private set; }

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
    }

    public bool ReloadScene()
    {
        //DG.Tweening.DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        return true;
    }
}
