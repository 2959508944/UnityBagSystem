using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSystem : MonoBehaviour
{
    public GameObject functionalButtonPrefab; // �����Ե��߰�ť��Prefab
    public Transform uiPanel; // UI��壬���ڷ��ù����Ե��߰�ť
    public Button interactionButton; // ͨ�ý�����ť�����ڴ�����Ŀ��Ľ���

    private InventoryBag inventory; // ��ұ���
    private Dictionary<string, GameObject> functionalItemButtons = new Dictionary<string, GameObject>(); // ����׷�ٹ����Ե��߰�ť

    void Start()
    {
        inventory = new InventoryBag(); // ��ʼ������
        // �����ͨ�ý�����ťʱ��ִ��InteractWithNearbyItems����
        interactionButton.onClick.AddListener(() => InteractWithNearbyItems());
    }

    // ����һ������
    public void PickupItem(Item item)
    {
        inventory.AddItem(item); // ��������ӵ�����

        // ���ݵ�������ִ�в�ͬ���߼�
        if (item.type == "Functional")
        {
            CreateFunctionalButton(item.name); // ���������Ե��ߵİ�ť
        }
        else if (item.type == "Passive")
        {
            ApplyPassiveEffect(item.name); // Ӧ�ñ���Ч��
        }
    }

    // ���������Ե��ߵİ�ť
    void CreateFunctionalButton(string itemName)
    {
        // ʵ����һ���µİ�ť����ӵ�UI���
        GameObject newButton = Instantiate(functionalButtonPrefab, uiPanel);
        // ����ť�����ʱ��ִ��UseFunctionalItem����
        newButton.GetComponent<Button>().onClick.AddListener(() => UseFunctionalItem(itemName));
        // ���ð�ť���ı�
        newButton.GetComponentInChildren<Text>().text = itemName;
        // ���°�ť��ӵ�׷���ֵ�
        functionalItemButtons[itemName] = newButton;
    }

    // ʹ�ù����Ե���
    void UseFunctionalItem(string itemName)
    {
        // ����Ӧ����ִ�е��߹��ܵĴ���
        Debug.Log($"Used {itemName}");
    }

    // ʹ���븽��Ŀ��Ľ�������
    void InteractWithNearbyItems()
    {
        // ���������е����е���
        foreach (Item item in inventory.bagList)
        {
            // ֻ���ǿɽ�������
            if (item.type == "Interactive")
            {
                // �������Ƿ��㹻����Ŀ������
                if (Vector3.Distance(transform.position, item.target.transform.position) < 3.0f)
                {
                    // ִ�н����߼�
                    Debug.Log($"Used {item.name} on {item.target.name}");
                }
            }
        }
    }

    // Ӧ�ñ���Ч��
    void ApplyPassiveEffect(string itemName)
    {
        // ����Ӧ����Ӧ�ñ���Ч���Ĵ���
        Debug.Log($"Applied passive effect of {itemName}");
    }
}
