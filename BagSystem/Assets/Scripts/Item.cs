using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public int itemID=0; //物品ID
    public int itemUseId=0;//道具ID
    public string itemName;//道具名称
    public Sprite ItemSprite;//物品的照片
    public int itemHeld = 0;//物品的数量

    [TextArea]//使text可以富文本进行多行书写
    public string itemInfo;//道具说明

    public float timer=-1;//物品时限
    public itemType thisType; //道具类型（用于背包分类）
    public string type; // 道具类型：Functional、Interactive、Passive(功能、可互动、被动)
    public bool isfirst=true; //是否第一次获取

    public int isNumber = 0;//当前物品第几个格子的物品

    public GameObject target; // 只针对可交互类型的道具，表示交互目标

    public enum itemType
    {
        consumables,//消耗品1
        task,   //任务物品2
        Star,//收藏品3
    }

    public Item()
    {

    }

    public Item(Item obj,int number)
    {
        this.itemID = obj.itemID;
        this.itemName = obj.itemName;
        this.ItemSprite = obj.ItemSprite;
        this.itemHeld = 0;
        this.itemInfo = obj.itemInfo;
        this.timer = obj.timer;
        this.thisType = obj.thisType;
        this.isfirst = obj.isfirst;
        this.isNumber = obj.isNumber++;
    }

}
//物品脚本
