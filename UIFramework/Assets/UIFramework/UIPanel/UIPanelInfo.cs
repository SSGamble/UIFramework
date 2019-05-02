using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Json 对应的 对象
/// </summary>
[System.Serializable] // 序列化
public class UIPanelInfo :ISerializationCallbackReceiver
{

    [System.NonSerialized] // 不序列化 UIPanelType，因为 Unity 中的 JsonUtility 无法解析枚举
    public UIPanelType panelType;

    // 将枚举中的元素转换为 String，再对这个字符串进行序列化，实际上得到的还是枚举中的值
    public string panelTypeString;

    public string path;

    /// <summary>
    /// 序列化之前调用
    /// </summary>
    public void OnBeforeSerialize()
    {
        
    }

    /// <summary>
    /// 反序列化之后调用，反序列化：从文本信息到对象
    /// </summary>
    public void OnAfterDeserialize()
    {
        // 将反序列化后的字符串转为对应的枚举类型
        UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        panelType = type;
    }
}
