using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSystem : MonoBehaviour
{
    public GameObject functionalButtonPrefab; // 功能性道具按钮的Prefab
    public Transform uiPanel; // UI面板，用于放置功能性道具按钮
    public Button interactionButton; // 通用交互按钮，用于触发与目标的交互

    private InventoryBag inventory; // 玩家背包
    private Dictionary<string, GameObject> functionalItemButtons = new Dictionary<string, GameObject>(); // 用于追踪功能性道具按钮

    void Start()
    {
        inventory = new InventoryBag(); // 初始化背包
        // 当点击通用交互按钮时，执行InteractWithNearbyItems函数
        interactionButton.onClick.AddListener(() => InteractWithNearbyItems());
    }

    // 捡起一个道具
    public void PickupItem(Item item)
    {
        inventory.AddItem(item); // 将道具添加到背包

        // 根据道具类型执行不同的逻辑
        if (item.type == "Functional")
        {
            CreateFunctionalButton(item.name); // 创建功能性道具的按钮
        }
        else if (item.type == "Passive")
        {
            ApplyPassiveEffect(item.name); // 应用被动效果
        }
    }

    // 创建功能性道具的按钮
    void CreateFunctionalButton(string itemName)
    {
        // 实例化一个新的按钮并添加到UI面板
        GameObject newButton = Instantiate(functionalButtonPrefab, uiPanel);
        // 当按钮被点击时，执行UseFunctionalItem函数
        newButton.GetComponent<Button>().onClick.AddListener(() => UseFunctionalItem(itemName));
        // 设置按钮的文本
        newButton.GetComponentInChildren<Text>().text = itemName;
        // 将新按钮添加到追踪字典
        functionalItemButtons[itemName] = newButton;
    }

    // 使用功能性道具
    void UseFunctionalItem(string itemName)
    {
        // 这里应放置执行道具功能的代码
        Debug.Log($"Used {itemName}");
    }

    // 使用与附近目标的交互道具
    void InteractWithNearbyItems()
    {
        // 遍历背包中的所有道具
        foreach (Item item in inventory.bagList)
        {
            // 只考虑可交互道具
            if (item.type == "Interactive")
            {
                // 检查玩家是否足够靠近目标物体
                if (Vector3.Distance(transform.position, item.target.transform.position) < 3.0f)
                {
                    // 执行交互逻辑
                    Debug.Log($"Used {item.name} on {item.target.name}");
                }
            }
        }
    }

    // 应用被动效果
    void ApplyPassiveEffect(string itemName)
    {
        // 这里应放置应用被动效果的代码
        Debug.Log($"Applied passive effect of {itemName}");
    }
}
