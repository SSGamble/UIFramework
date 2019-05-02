using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TaskPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        EventCenter.AddListener<string>(EventType.ShowTaskPanel, ShowTaskPanel);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventType.ShowTaskPanel, ShowTaskPanel);
    }

    private void ShowTaskPanel(string panelTypeStr)
    {
        print(panelTypeStr);
    }

    /// <summary>
    /// 面板进入
    /// </summary>
    public override void OnEnter()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        
        canvasGroup.alpha = 0; // 隐藏
        canvasGroup.blocksRaycasts = true; // 开启鼠标交互
        canvasGroup.DOFade(1, 0.5f); // 渐显动画
    }

    /// <summary>
    /// 处理面板的关闭
    /// </summary>
    public override void OnExit()
    {
        canvasGroup.blocksRaycasts = false; // 停止鼠标交互
        // 渐隐动画
        canvasGroup.DOFade(0, 0.5f).OnComplete(()=> {
            Destroy(gameObject); // 销毁面板
        });
    }

    /// <summary>
    /// 关闭当前面板，关闭按钮点击事件
    /// </summary>
	public void OnClosePanel()
    {
        UIManager.Instance.PopPanel(); // 出栈
    }
}
