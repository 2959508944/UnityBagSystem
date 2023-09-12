using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "New InventoryBag", menuName = "Inventory/New IventoryBag")]
public class InventoryBag : ScriptableObject
{
    /// <summary>
    /// ��ұ�������Ϊ��������Ʒ��������һ������
    /// </summary>
    public List<Item> bagList = new List<Item>();

    // ���һ�����ߵ�����
    public void AddItem(Item item)
    {
        bagList.Add(item);

        // ������Ʒ������ȷ��Ӧ����δ���
        if (item.type == "Functional")
        {
            // ����ǹ�������Ʒ���򴴽�һ�����ð�ť���ڼ�����
            ButtonSystem.CreateButton(item.name, false);
        }
        else if (item.type == "TemporaryFunctional")
        {
            // �������ʱ��������Ʒ���򴴽�һ����ʱ��ť���ڼ�����
            ButtonSystem.CreateButton(item.name, true);
        }
        else if (item.type == "Passive")
        {
            // ����Ǳ�����Ʒ����ֱ��Ӧ����Ч��
            ApplyPassiveEffect(item.name);
        }
    }

    void ApplyPassiveEffect(string itemName)
    {
        // ������Ӧ�ñ���Ч�����������ӽ�ɫ���Ե�
    }
}
