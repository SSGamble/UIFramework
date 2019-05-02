using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 整个 UI框架 的管理器
///     解析 Json 面板信息，保存到 panelPathDict 字典里
///     创建保存所有面板的实例，panelDict 字典
///     管理保存所有显示的面板，栈
/// </summary>
public class UIManager{

    // 单例
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }


    // 单例，私有化构造方法
    private UIManager()
    {
        ParseUIPanelTypeJson();
    }

    // 会将生成的面板放在 Canvas 的下面，用于设置一个父子关系
    private Transform canvasTransform;
    public Transform CanvasTransform
    {
        get
        {
            if(canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }

    // 存储所有 面板 Prefab 的路径；key：面板名，value：路径
    private Dictionary<UIPanelType, string> panelPathDict;
    // 存储所有实例化的面板的游戏物体身上的 BasePanel 组件；key：面板名，value：面板组件(游戏物体)
    private Dictionary<UIPanelType, BasePanel> panelDict;
    private Stack<BasePanel> panelStack;

    /// <summary>
    /// 把某个页面入栈，把某个页面显示在界面上
    /// </summary>
    public void PushPanel(UIPanelType panelType)
    {
        // 初始化栈
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        // 判断一下栈里面是否已有面板，需暂停当前面板
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek(); // 获取栈顶面板
            topPanel.OnPause(); // 暂停面板
        }

        BasePanel panel = GetPanel(panelType); // 得到面板
        panel.OnEnter(); // Panel 生命周期函数
        panelStack.Push(panel); // 入栈
    }

    /// <summary>
    /// 把某个页面出栈，栈顶面板出栈，启用第二个面板
    /// </summary>
    public void PopPanel()
    {
        // 安全效验
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }

        // 当前栈里没有面板
        if (panelStack.Count <= 0) return;

        // 关闭栈顶面板的显示
        BasePanel topPanel = panelStack.Pop(); // 栈顶出栈
        topPanel.OnExit(); // 退出面板

        // 当前栈里没有面板
        if (panelStack.Count <= 0) return;

        // 继续新的栈顶面板
        BasePanel topPanel2 = panelStack.Peek(); // 获取当前栈顶面板
        topPanel2.OnResume(); // 继续当前面板
    }


    /// <summary>
    /// 根据面板类型得到实例化的面板
    ///     字典 panelDict 里有就直接获取，没有就创建
    /// </summary>
    /// <param name="panelType"></param>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {
        // 初始化字典
        if(panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        BasePanel panel = panelDict.TryGet(panelType);

        // 如果找不到，就找这个面板 Prefab 的路径，然后利用 Resources 加载 Prefab 去实例化面板
        if (panel == null) 
        {
            string path = panelPathDict.TryGet(panelType); // 得到面板对应的路径
            GameObject insPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject; // 根据路径，实例化面板
            insPanel.transform.SetParent(CanvasTransform,false); // 放在 Canvas 下面

            // 判断键是否存在
            if (panelDict.ContainsKey(panelType))
            {
                // 若存在则直接修改 value 值
                panelDict[panelType] = insPanel.GetComponent<BasePanel>();
            }
            else
            {
                // 不存在，放在面板字典里
                panelDict.Add(panelType, insPanel.GetComponent<BasePanel>());
            }

            return insPanel.GetComponent<BasePanel>(); // 返回刚刚实例化的面板
        }
        else // 从字典里找到了，直接返回
        {
            return panel;
        }
    }

    /// <summary>
    /// 内部类，Json 对象
    /// </summary>
    [System.Serializable] // 序列化
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList;
    }

    /// <summary>
    /// 解析 Json
    /// </summary>
    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>(); // new 一个空的字典
        TextAsset ta = Resources.Load<TextAsset>("UIPanelType"); // Json 文本
        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text); // json 转 对象
        // 遍历 json 中的面板信息[面板，路径]，添加到字典里
        foreach (UIPanelInfo info in jsonObject.infoList)
        {
            panelPathDict.Add(info.panelType, info.path);
        }
    }

    public void Test()
    {
        string path;
        panelPathDict.TryGetValue(UIPanelType.Knapsack, out path);
        Debug.Log(path);
    }
}
