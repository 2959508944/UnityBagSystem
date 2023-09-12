using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public int itemID=0; //��ƷID
    public int itemUseId=0;//����ID
    public string itemName;//��������
    public Sprite ItemSprite;//��Ʒ����Ƭ
    public int itemHeld = 0;//��Ʒ������

    [TextArea]//ʹtext���Ը��ı����ж�����д
    public string itemInfo;//����˵��

    public float timer=-1;//��Ʒʱ��
    public itemType thisType; //�������ͣ����ڱ������ࣩ
    public string type; // �������ͣ�Functional��Interactive��Passive(���ܡ��ɻ���������)
    public bool isfirst=true; //�Ƿ��һ�λ�ȡ

    public int isNumber = 0;//��ǰ��Ʒ�ڼ������ӵ���Ʒ

    public GameObject target; // ֻ��Կɽ������͵ĵ��ߣ���ʾ����Ŀ��

    public enum itemType
    {
        consumables,//����Ʒ1
        task,   //������Ʒ2
        Star,//�ղ�Ʒ3
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
//��Ʒ�ű�
