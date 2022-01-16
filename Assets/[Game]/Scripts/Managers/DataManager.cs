using UnityEngine;

public class DataManager : MonoBehaviour
{
    private readonly string LEVEL_DATA = "level_data";
    private readonly string CURRENCY_DATA = "currency_data";
    private readonly string STACK_UPGRADE_DATA = "stack_upgrade_data";

    public int Level { get; private set; }
    public int Currency { get; private set; }
    public int StackUpgrade { get; private set; }

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
        Currency = PlayerPrefs.GetInt(CURRENCY_DATA, 0);
        StackUpgrade = PlayerPrefs.GetInt(STACK_UPGRADE_DATA, 0);
    }

    public void SetLevel(int _level)
    {
        Level = _level;
        PlayerPrefs.SetInt(LEVEL_DATA, _level);
        PlayerPrefs.Save();
    }

    public void SetCurrency(int _coin)
    {
        Currency = _coin;
        PlayerPrefs.SetInt(CURRENCY_DATA, _coin);
        PlayerPrefs.Save();
    }

    public void SetStackUpgrade(int _stackUpgrade)
    {
        StackUpgrade = _stackUpgrade;
        PlayerPrefs.SetInt(STACK_UPGRADE_DATA, _stackUpgrade);
        PlayerPrefs.Save();
    }
}
