using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 启动入口 GameRoot，需挂载在游戏物体上
/// </summary>
public class GameRoot : MonoBehaviour {

	void Start () {
        UIManager.Instance.PushPanel(UIPanelType.MainMenu); // 主菜单入栈
    }
	
	void Update () {
		
	}
}
