using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    public InventoryBag[] BagPages;
    public InventoryBag mainItem;
    public static ModelManager modelManager;
    private void Awake()
    {
        modelManager = this;
        mainItem = BagPages[0];
    }

    /// <summary>
    /// 增加一个物品进背包
    /// </summary>
    /// <param name="objID">物品ID</param>
    public Item AddObj(Item obj)//传入想添加的物品
    {
        int index = 0;
        Item item = obj;
        if (!mainItem.bagList.Contains(obj))//如果没找到物品则添加物品
        {
            index = 1;
            mainItem.bagList.Add(obj);
        }
        else//如果找到物品
        {
            bool isFind = false;
            int maxNumber = -1;
            for(int i = 0;i< mainItem.bagList.Count; i++)
            {
                Item Now = mainItem.bagList[i];
                if (Now.itemName == obj.itemName && Now.itemHeld < 99)
                {
                    isFind = true;
                    Now.itemHeld += 1;
                    maxNumber = Now.isNumber;
                    index = 2;
                    break;
                }
            }
            if (!isFind)
            {
                item = new Item(obj, maxNumber);
                mainItem.bagList.Add(item);
                item.itemHeld += 1;
                index = 3;
            }
        }

        print("获得"+obj.name);
       
        DataSortforID();
        if (index == 1) return obj;
        else return item;
    }
    /// <summary>
    /// 从背包中删除一个物品
    /// </summary>
    /// <param name="objID">物品ID</param>
    public void RemoveObj(Item obj)
    {
        if (mainItem.bagList.Contains(obj))
        {
            print("删除" + obj.name);
            if (obj.itemHeld>1)
            {
                obj.itemHeld -= 1;
            }
            else
            {
                obj.itemHeld -= 1;
                mainItem.bagList.Remove(obj);
            }
            
        }
        else
        {
            print("尝试删除空物体"+obj.name);
        }

        DataSortforID();
    }
    /// <summary>
    /// 按ID给物品排序
    /// </summary>
    public void DataSortforID()
    {
       mainItem.bagList.Sort((a, b) => {
           if (a.itemID != b.itemID) return a.itemID.CompareTo(b.itemID);
           else return b.isNumber.CompareTo(a.isNumber);
       }); 
    }


}
