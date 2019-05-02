using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnapsackPanel : BasePanel
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

        // 移动动画
        Vector3 temp = transform.localPosition;
        temp.x = 600;
        transform.localPosition = temp;
        transform.DOLocalMoveX(0, 0.5f);
    }

    /// <summary>
    /// 面板暂停
    /// </summary>
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false; // 停止鼠标交互
    }

    /// <summary>
    /// 面板恢复
    /// </summary>
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 面板退出
    /// </summary>
    public override void OnExit()
    {
        canvasGroup.blocksRaycasts = false; // 停止鼠标交互

        // 移动动画
        transform.DOLocalMoveX(600, 0.5f).OnComplete(()=> {
            canvasGroup.alpha = 0; // 隐藏
            Destroy(gameObject);
        });
    }

    /// <summary>
    /// 关闭当前面板
    /// </summary>
	public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }

    /// <summary>
    /// 弹出详细信息
    /// </summary>
    public void OnItemButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.ItemMessage);
    }
}
