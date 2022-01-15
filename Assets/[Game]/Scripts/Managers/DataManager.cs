using UnityEngine;

public class DataManager : MonoBehaviour
{
    private readonly string LEVEL_DATA = "level_data";

    public int Level { get; private set; }

    #region Singleton
    private static DataManager instance;
    public static DataManager Instance {
        get {
            if (instance == null)
            {
                GameObject obj = new GameObject("DataManager");
                instance = obj.AddComponent<DataManager>();
            }

            return instance;
        }
    }
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this);
        GetData();
    }

    private void GetData()
    {
        Level = PlayerPrefs.GetInt(LEVEL_DATA, 1);
    }

    public void SetLevel(int _level)
    {
        Level = _level;
        PlayerPrefs.SetInt(LEVEL_DATA, Level);
        PlayerPrefs.Save();
    }
}
