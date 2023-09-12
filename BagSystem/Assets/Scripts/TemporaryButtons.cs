using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryButtons : MonoBehaviour
{
    // ���ڴ洢������ʱ��ť���ֵ�
    private Dictionary<string, GameObject> temporaryButtons = new Dictionary<string, GameObject>();

    // ����һ���������ڴӱ�����UI���Ƴ���ʱ��Ʒ�����Ӧ�İ�ť
    public void RemoveTemporaryItem(string itemName)
    {
        // ����ֵ����Ƿ���������ʱ��ť
        if (temporaryButtons.ContainsKey(itemName))
        {
            // ������ڣ����ٰ�ť�����ֵ����Ƴ�
            Destroy(temporaryButtons[itemName]);
            temporaryButtons.Remove(itemName);
        }
    }
}
