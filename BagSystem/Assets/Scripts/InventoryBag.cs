using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "New InventoryBag", menuName = "Inventory/New IventoryBag")]
public class InventoryBag : ScriptableObject
{
    /// <summary>
    /// 玩家背包，因为储存多个物品，所以是一个集合
    /// </summary>
    public List<Item> bagList = new List<Item>();

    // 添加一个道具到背包
    public void AddItem(Item item)
    {
        bagList.Add(item);

        // 根据物品类型来确定应该如何处理
        if (item.type == "Functional")
        {
            // 如果是功能性物品，则创建一个永久按钮用于激活它
            ButtonSystem.CreateButton(item.name, false);
        }
        else if (item.type == "TemporaryFunctional")
        {
            // 如果是临时功能性物品，则创建一个临时按钮用于激活它
            ButtonSystem.CreateButton(item.name, true);
        }
        else if (item.type == "Passive")
        {
            // 如果是被动物品，则直接应用其效果
            ApplyPassiveEffect(item.name);
        }
    }

    void ApplyPassiveEffect(string itemName)
    {
        // 在这里应用被动效果，例如增加角色属性等
    }
}
