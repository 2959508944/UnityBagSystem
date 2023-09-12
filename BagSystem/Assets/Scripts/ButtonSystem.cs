using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ButtonSystem : MonoBehaviour
{
    //按钮字典
    private static Dictionary<string, GameObject> temporaryButtons = new Dictionary<string, GameObject>();
    private static Dictionary<string, GameObject> specificButtons = new Dictionary<string, GameObject>();

    //按钮预制体
    public static GameObject temporarySpecificButtonPrefab;
    public static GameObject specificButtonPrefab;

    //面板
    public static Transform uiPanel;

    // 定义一个函数来与附近的物体进行交互
    void InteractWithNearbyItems()
    {
        // 使用Physics.OverlapSphere()检查指定范围内是否有标记为"Pushable"的物体
        // 这个范围是以角色位置为中心，半径为3.0f的球体
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3.0f);

        // 遍历这些碰撞体以查找可推动物体
        foreach (var hitCollider in hitColliders)
        {
            // 检查物体是否有"Pushable"标签
            if (hitCollider.CompareTag("Pushable"))
            {
                // 如果有，则执行推动物体的逻辑
                Debug.Log("Pushed a pushable object.");
            }
        }
    }

    // 创建一个函数用于生成UI按钮，这些按钮允许用户与特定物品进行交互
    // itemName表示物品的名称，isTemporary表示这是不是一个临时按钮
    public static void CreateButton(string itemName, bool isTemporary)
    {
        GameObject newButton; // 新按钮的引用

        // 根据是否是临时按钮来选择对应的预制体
        if (isTemporary)
        {
            newButton = Instantiate(temporarySpecificButtonPrefab, uiPanel);
            //temporarySpecificButtonPrefab：这是一个预制体（Prefab）,它是一个具有特定设置、组件和子对象的按钮。
            //uiPanel：这是一个已存在于场景中或先前创建的游戏对象，它作为新创建按钮的父对象。
        }
        else
        {
            newButton = Instantiate(specificButtonPrefab, uiPanel);
            //不同的预制体specificButtonPrefab
        }

        // 给新按钮添加一个点击事件监听器，用于触发物品使用
        newButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => UseSpecificItem(itemName, isTemporary));
        // 设置按钮上显示的文本为物品名
        newButton.GetComponentInChildren<Text>().text = itemName;

        // 添加缩放和光效动画，这里需要为按钮添加一个Animator组件
        Animator buttonAnimator = newButton.GetComponent<Animator>();
        buttonAnimator.Play("ButtonEffectAnimation");

        // 根据按钮类型存储按钮的引用，以便稍后使用或销毁
        if (isTemporary)
        {
            temporaryButtons[itemName] = newButton;//临时按钮
        }
        else
        {
            specificButtons[itemName] = newButton;//非临时按钮
        }
    }

    // 定义一个函数用于使用特定的物品
    // itemName表示物品的名称，isTemporary表示这是不是一个临时物品
    static void UseSpecificItem(string itemName, bool isTemporary)
    {
        // 这里添加使用该物品的逻辑
        Debug.Log($"Used specific item {itemName}");

        // 如果这是一个临时物品，那么使用后应该销毁这个按钮
        if (isTemporary)
        {
            Destroy(temporaryButtons[itemName]);
            temporaryButtons.Remove(itemName);
        }
    }

}
