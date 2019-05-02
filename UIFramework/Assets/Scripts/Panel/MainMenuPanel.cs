using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 将指定面板入栈
    /// </summary>
    /// <param name="panelTypeStr"></param>
    public void OnPushPanel(string panelTypeStr)
    {
        // 将字符串转换为枚举类型
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeStr);
        // 将面板入栈
        UIManager.Instance.PushPanel(panelType); 
    }

    /// <summary>
    /// 暂停面板
    /// </summary>
    public override void OnPause()
    {
        // 暂停面板时，让主菜单面板不再和鼠标交互
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 继续面板
    /// </summary>
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true; // 启用鼠标交互
    }
}
