using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIControll : MonoBehaviour
{
    const int maxItemSum = 19;//��Ʒ��������

    public Dictionary<Item,bool> achievement= new Dictionary<Item, bool>();  //�ɾͣ���¼�Ƿ��һ�λ�ȡ
    public Item[] items; //��Ʒ����   ����ģ��ʰȡ����

    public GameObject bagpage; //��������
    List<GameObject> itemUI=new List<GameObject>(); //�����ĸ���
    List<Item> viewBag=new List<Item>();  //��ǰչʾ������

    List<Item> BagData;//���������б�

    
    public enum ObjCategory   //�������
    {
        all,  //ȫ��
        xhp, //����Ʒ
        rwwp,  //������Ʒ
        star,  //�ղ�
        xs,   //��ʱ��Ʒ
        
    }
    ObjCategory _category = ObjCategory.all;
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ������Ϊ��һ�λ�ȡ
        for (int i = 0; i < items.Length; i++)
        {
            achievement.Add(items[i], true);
        }

        BagData = ModelManager.modelManager.mainItem.bagList;
        //��¼���пո��ӣ����ڸ�ֵͼƬ����Ʒ����
        for (int i = 0; i < bagpage.transform.childCount; i++)
        {
            itemUI.Add(bagpage.transform.GetChild(i).gameObject);
        }

        UICategory(_category);
    }
    /// <summary>
    /// ˢ�±���UI
    /// </summary>
     void UpdateBagUI()
    {
        ResetPlaid();
        if (viewBag.Count > 0)
        {
            for (int i = 0; i < viewBag.Count; i++)
            {
                if (viewBag[i].itemHeld < 1)
                {
                    BagData.Remove(viewBag[i]);
                    viewBag.Remove(viewBag[i]);
                }
            }
            for (int i = 0; i < viewBag.Count; i++)
            {
                bagpage.transform.GetChild(i).GetComponent<Image>().sprite = viewBag[i].ItemSprite;
                // bagpage.transform.GetChild(i).name = viewBag[i].itemID.ToString();
                bagpage.transform.GetChild(i).GetChild(0).transform.GetComponent<Text>().text = viewBag[i].itemHeld.ToString();

                bagpage.transform.GetChild(i).GetChild(1).gameObject.SetActive(viewBag[i].isfirst);
                
            }
        }
        ShowInformation(OnShowitem);
    }

    void ResetPlaid()
    {
        foreach (var item in itemUI)
        {
            item.GetComponent<Image>().sprite = null;
            item.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {     //ͨ���������ui�ڸ����б��е��±��жϵ�ǰ����ĵڼ�������  
              //ͬʱ���ݸ��±��õ��ڴ�չʾ�б��ж�Ӧ�±������
            if (EventSystem.current.currentSelectedGameObject!=null)
            {
                for (int i = 0; i < viewBag.Count; i++)
                {
                    if (itemUI[i]== EventSystem.current.currentSelectedGameObject)
                    {
                        OnShowitem = viewBag[i];
                        OnShowitem.isfirst = false;

                        break;
                    }
                    else if(EventSystem.current.currentSelectedGameObject.tag=="bag")
                    {
                        OnShowitem = null;
                    }
                }
            }
            else
            {
                OnShowitem = null;
            }
            UICategory(_category);
        }
    }

    /// <summary>
    /// �����Ʒ
    /// </summary>
    public void OnClickAddObj()
    {
        Item item=  ModelManager.modelManager.AddObj(items[UnityEngine.Random.Range(0, items.Length)]);
        Console.Write(items.Length);
        bool isfirst = item.isfirst;
      
        UICategory(_category);

        //ҳ���Ϸ���չʾ����
        if (!AnimPanel.activeInHierarchy)
        {
            AnimPanel.transform.GetChild(0).GetComponent<Image>().sprite = item.ItemSprite;
            AnimPanel.transform.GetChild(1).GetComponent<Text>().text = "��ϲ���" + item.itemName;
            AnimPanel.SetActive(true);
            StartCoroutine(AddAnimPanel(AnimPanel, 2f));
        }
    }
    public GameObject AnimPanel;
    IEnumerator AddAnimPanel(GameObject obj,float timer)
    {
        yield return new WaitForSeconds(timer);
        obj.SetActive(false);
    }

    public Item OnShowitem;  //��ǰѡ�е�����

    /// <summary>
    /// ����
    /// </summary>
    public void UICategory(ObjCategory _category)
    {
        viewBag.Clear();
        switch (_category)
        {
            case ObjCategory.xhp:
                for (int i = 0; i < BagData.Count; i++)
                {
                    if (BagData[i].thisType== Item.itemType.consumables)
                    {
                        viewBag.Add(BagData[i]);
                    }
                }
                break;
            case ObjCategory.rwwp:
                for (int i = 0; i < BagData.Count; i++)
                {
                    if (BagData[i].thisType == Item.itemType.task)
                    {
                        viewBag.Add(BagData[i]);
                    }
                }
                break;
            case ObjCategory.star:
                for (int i = 0; i < BagData.Count; i++)
                {
                    if (BagData[i].thisType == Item.itemType.Star)
                    {
                        viewBag.Add(BagData[i]);
                    }
                }
                break;
            case ObjCategory.xs:
                for (int i = 0; i < BagData.Count; i++)
                {
                    if (BagData[i].timer>0)
                    {
                        viewBag.Add(BagData[i]);
                    }
                }
                break;
            case ObjCategory.all:
                
                foreach (var item in BagData)
                {
                    viewBag.Add(item);
                }
                break;
        }

        UpdateBagUI();
    }

    public void OnClickCategory(string cateType)
    {
        switch (cateType)
        {
            case "ȫ��":
                _category = ObjCategory.all;
                break;
            case "����Ʒ":
                _category = ObjCategory.xhp;
                break;
            case "����":
                _category = ObjCategory.rwwp;
                break;
            case "�ղ�":
                _category = ObjCategory.star;
                break;
            case "��ʱ":
                _category = ObjCategory.xs;
                break;
            default:
                _category = ObjCategory.all;
                break;
        }
        UICategory(_category);
    }

    /// <summary>
    /// ��Ʒ��Ϣչʾ
    /// </summary>
    /// <param name="item"></param>
    public void ShowInformation(Item item)
    {
        if (item == null)
        {
            ObjName.text = "";
            ObjIntroduce.text = "";
            ObjImg.sprite = null;
        }
        else
        {
            ObjName.text ="��Ʒ���ƣ�"+ item.itemName;
            ObjIntroduce.text = item.itemInfo;
            ObjImg.sprite = item.ItemSprite;
            if (item.timer > 0)
            {
                ObjIntroduce.text = item.itemInfo + "\n" + "ʣ��" + item.timer + "�����";
            }
        }
    }
    public Text ObjName;//��Ʒ����
    public Text ObjIntroduce;//��Ʒ����
    public Image ObjImg;//��ƷͼƬ

    /*public int coinNumber = 1000;//�������

    public void Addcoins(Text coinText)
    {
        int.TryParse(coinText.text, out int coins);
        coins += Random.Range(0, 10000);
        ShowCoin(coinText,coins);
    }

    public void ShowCoin(Text coinText,int coins)
    {
        coinNumber = coins;
        coinText.text = coins.ToString();
    }
    */

    public void OnClickUpPage()
    {

    }
    public void OnClickNextPage()
    {

    }

}
