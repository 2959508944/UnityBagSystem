using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIControll : MonoBehaviour
{
    const int maxItemSum = 19;//物品种类数；

    public Dictionary<Item,bool> achievement= new Dictionary<Item, bool>();  //成就，记录是否第一次获取
    public Item[] items; //物品数组   用于模拟拾取操作

    public GameObject bagpage; //背包物体
    List<GameObject> itemUI=new List<GameObject>(); //背包的格子
    List<Item> viewBag=new List<Item>();  //当前展示的物体

    List<Item> BagData;//背包数据列表

    
    public enum ObjCategory   //分类类别
    {
        all,  //全部
        xhp, //消耗品
        rwwp,  //任务物品
        star,  //收藏
        xs,   //限时物品
        
    }
    ObjCategory _category = ObjCategory.all;
    // Start is called before the first frame update
    void Start()
    {
        //初始都设置为第一次获取
        for (int i = 0; i < items.Length; i++)
        {
            achievement.Add(items[i], true);
        }

        BagData = ModelManager.modelManager.mainItem.bagList;
        //记录所有空格子，用于赋值图片和物品数量
        for (int i = 0; i < bagpage.transform.childCount; i++)
        {
            itemUI.Add(bagpage.transform.GetChild(i).gameObject);
        }

        UICategory(_category);
    }
    /// <summary>
    /// 刷新背包UI
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
        {     //通过点击到的ui在格子列表中的下标判断当前点击的第几个格子  
              //同时根据该下标拿到在待展示列表中对应下标的物体
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
    /// 添加物品
    /// </summary>
    public void OnClickAddObj()
    {
        Item item=  ModelManager.modelManager.AddObj(items[UnityEngine.Random.Range(0, items.Length)]);
        Console.Write(items.Length);
        bool isfirst = item.isfirst;
      
        UICategory(_category);

        //页面上方的展示动画
        if (!AnimPanel.activeInHierarchy)
        {
            AnimPanel.transform.GetChild(0).GetComponent<Image>().sprite = item.ItemSprite;
            AnimPanel.transform.GetChild(1).GetComponent<Text>().text = "恭喜获得" + item.itemName;
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

    public Item OnShowitem;  //当前选中的物体

    /// <summary>
    /// 分类
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
            case "全部":
                _category = ObjCategory.all;
                break;
            case "消耗品":
                _category = ObjCategory.xhp;
                break;
            case "任务":
                _category = ObjCategory.rwwp;
                break;
            case "收藏":
                _category = ObjCategory.star;
                break;
            case "限时":
                _category = ObjCategory.xs;
                break;
            default:
                _category = ObjCategory.all;
                break;
        }
        UICategory(_category);
    }

    /// <summary>
    /// 物品信息展示
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
            ObjName.text ="商品名称："+ item.itemName;
            ObjIntroduce.text = item.itemInfo;
            ObjImg.sprite = item.ItemSprite;
            if (item.timer > 0)
            {
                ObjIntroduce.text = item.itemInfo + "\n" + "剩余" + item.timer + "天过期";
            }
        }
    }
    public Text ObjName;//物品名称
    public Text ObjIntroduce;//物品介绍
    public Image ObjImg;//物品图片

    /*public int coinNumber = 1000;//金币数量

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
