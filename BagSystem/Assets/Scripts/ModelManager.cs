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
    /// ����һ����Ʒ������
    /// </summary>
    /// <param name="objID">��ƷID</param>
    public Item AddObj(Item obj)//��������ӵ���Ʒ
    {
        int index = 0;
        Item item = obj;
        if (!mainItem.bagList.Contains(obj))//���û�ҵ���Ʒ�������Ʒ
        {
            index = 1;
            mainItem.bagList.Add(obj);
        }
        else//����ҵ���Ʒ
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

        print("���"+obj.name);
       
        DataSortforID();
        if (index == 1) return obj;
        else return item;
    }
    /// <summary>
    /// �ӱ�����ɾ��һ����Ʒ
    /// </summary>
    /// <param name="objID">��ƷID</param>
    public void RemoveObj(Item obj)
    {
        if (mainItem.bagList.Contains(obj))
        {
            print("ɾ��" + obj.name);
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
            print("����ɾ��������"+obj.name);
        }

        DataSortforID();
    }
    /// <summary>
    /// ��ID����Ʒ����
    /// </summary>
    public void DataSortforID()
    {
       mainItem.bagList.Sort((a, b) => {
           if (a.itemID != b.itemID) return a.itemID.CompareTo(b.itemID);
           else return b.isNumber.CompareTo(a.isNumber);
       }); 
    }


}
