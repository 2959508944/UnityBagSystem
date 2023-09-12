using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ButtonSystem : MonoBehaviour
{
    //��ť�ֵ�
    private static Dictionary<string, GameObject> temporaryButtons = new Dictionary<string, GameObject>();
    private static Dictionary<string, GameObject> specificButtons = new Dictionary<string, GameObject>();

    //��ťԤ����
    public static GameObject temporarySpecificButtonPrefab;
    public static GameObject specificButtonPrefab;

    //���
    public static Transform uiPanel;

    // ����һ���������븽����������н���
    void InteractWithNearbyItems()
    {
        // ʹ��Physics.OverlapSphere()���ָ����Χ���Ƿ��б��Ϊ"Pushable"������
        // �����Χ���Խ�ɫλ��Ϊ���ģ��뾶Ϊ3.0f������
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3.0f);

        // ������Щ��ײ���Բ��ҿ��ƶ�����
        foreach (var hitCollider in hitColliders)
        {
            // ��������Ƿ���"Pushable"��ǩ
            if (hitCollider.CompareTag("Pushable"))
            {
                // ����У���ִ���ƶ�������߼�
                Debug.Log("Pushed a pushable object.");
            }
        }
    }

    // ����һ��������������UI��ť����Щ��ť�����û����ض���Ʒ���н���
    // itemName��ʾ��Ʒ�����ƣ�isTemporary��ʾ���ǲ���һ����ʱ��ť
    public static void CreateButton(string itemName, bool isTemporary)
    {
        GameObject newButton; // �°�ť������

        // �����Ƿ�����ʱ��ť��ѡ���Ӧ��Ԥ����
        if (isTemporary)
        {
            newButton = Instantiate(temporarySpecificButtonPrefab, uiPanel);
            //temporarySpecificButtonPrefab������һ��Ԥ���壨Prefab��,����һ�������ض����á�������Ӷ���İ�ť��
            //uiPanel������һ���Ѵ����ڳ����л���ǰ��������Ϸ��������Ϊ�´�����ť�ĸ�����
        }
        else
        {
            newButton = Instantiate(specificButtonPrefab, uiPanel);
            //��ͬ��Ԥ����specificButtonPrefab
        }

        // ���°�ť���һ������¼������������ڴ�����Ʒʹ��
        newButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => UseSpecificItem(itemName, isTemporary));
        // ���ð�ť����ʾ���ı�Ϊ��Ʒ��
        newButton.GetComponentInChildren<Text>().text = itemName;

        // ������ź͹�Ч������������ҪΪ��ť���һ��Animator���
        Animator buttonAnimator = newButton.GetComponent<Animator>();
        buttonAnimator.Play("ButtonEffectAnimation");

        // ���ݰ�ť���ʹ洢��ť�����ã��Ա��Ժ�ʹ�û�����
        if (isTemporary)
        {
            temporaryButtons[itemName] = newButton;//��ʱ��ť
        }
        else
        {
            specificButtons[itemName] = newButton;//����ʱ��ť
        }
    }

    // ����һ����������ʹ���ض�����Ʒ
    // itemName��ʾ��Ʒ�����ƣ�isTemporary��ʾ���ǲ���һ����ʱ��Ʒ
    static void UseSpecificItem(string itemName, bool isTemporary)
    {
        // �������ʹ�ø���Ʒ���߼�
        Debug.Log($"Used specific item {itemName}");

        // �������һ����ʱ��Ʒ����ôʹ�ú�Ӧ�����������ť
        if (isTemporary)
        {
            Destroy(temporaryButtons[itemName]);
            temporaryButtons.Remove(itemName);
        }
    }

}
