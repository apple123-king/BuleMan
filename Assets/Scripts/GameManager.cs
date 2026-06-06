using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public int score = 0;
    public int life = 9;
    public int currentLevel = 0;
    public int MaxLevel = 2;
    public int firstLevelBuildIndex = 1;
    public int startingLife = 9;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject singletonObj = new GameObject("UnitySingleton");
                    _instance = singletonObj.AddComponent<GameManager>();
                    DontDestroyOnLoad(singletonObj);
                }
            }

            return _instance;
        }
    }

    public int VictorySceneBuildIndex => firstLevelBuildIndex + MaxLevel;
    public int DefeatSceneBuildIndex => VictorySceneBuildIndex + 1;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ResetRun()
    {
        score = 0;
        life = startingLife;
        currentLevel = 0;
    }

    public void StartRun()
    {
        ResetRun();
        currentLevel = 1;
    }

    public void AdvanceLevel()
    {
        currentLevel++;
    }

    public void AddScore(int amount)
    {
        score = Mathf.Max(0, score + amount);
    }

    public void AddLife(int amount)
    {
        life = Mathf.Max(0, life + amount);
    }

    public bool SpendLife(int amount)
    {
        life -= Mathf.Max(1, amount);
        return life >= 0;
    }
}
