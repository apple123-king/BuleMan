using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public int score = 0;
    public int life =9;
    public int currentLevel = 0;
    public int MaxLevel = 2;//比实际关卡数多1

    // 公共只读访问点

    public static GameManager Instance
    {
        get
        {
            // 场景中没有实例时，自动创建GameObject并挂载
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject singletonObj = new GameObject("UnitySingleton");
                    _instance = singletonObj.AddComponent<GameManager>();
                    // 场景切换不销毁，实现跨场景持久化
                    DontDestroyOnLoad(singletonObj);
                }
            }
            return _instance;
        }
    }

    // Unity 生命周期：优先初始化
    private void Awake()
    {
        // 防止场景中有多个实例（比如手动拖了多个该组件）
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // 销毁重复实例
        }
    }

    // 示例：全局方法
   
}