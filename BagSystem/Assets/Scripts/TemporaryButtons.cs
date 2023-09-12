using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryButtons : MonoBehaviour
{
    // 用于存储所有临时按钮的字典
    private Dictionary<string, GameObject> temporaryButtons = new Dictionary<string, GameObject>();

    // 定义一个函数用于从背包和UI中移除临时物品和其对应的按钮
    public void RemoveTemporaryItem(string itemName)
    {
        // 检查字典中是否存在这个临时按钮
        if (temporaryButtons.ContainsKey(itemName))
        {
            // 如果存在，销毁按钮并从字典中移除
            Destroy(temporaryButtons[itemName]);
            temporaryButtons.Remove(itemName);
        }
    }
}
