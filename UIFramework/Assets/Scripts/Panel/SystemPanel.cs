using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemPanel : BasePanel
{

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 面板进入
    /// </summary>
    public override void OnEnter()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 处理面板的关闭
    /// </summary>
    public override void OnExit()
    {
        canvasGroup.alpha = 0; // 隐藏
        canvasGroup.blocksRaycasts = false; // 停止鼠标交互
        Destroy(gameObject);
    }

    /// <summary>
    /// 关闭当前面板
    /// </summary>
	public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }
}
