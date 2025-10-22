using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品类
/// 处理所有拖拽事件以及保存物品数据
/// </summary>
public class Item : MonoBehaviour
{
    /// <summary>
    /// 物品数据
    /// </summary>
    public ItemData itemData;
    /// <summary>
    /// 物品的图标
    /// </summary>
    public Image _icon;
    /// <summary>
    /// 物品的数量
    /// </summary>
    public TMP_Text _quantity;

    /// <summary>
    /// 物品所在的插槽
    /// </summary>
    public Slot bindSlot;

    private void Awake()
    {
        _icon       = transform.Find("Icon").GetComponent<Image>();
        _quantity   = transform.Find("Quantity").GetComponent<TMP_Text>();

        _icon.sprite = null;
        _quantity.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 设置item数量
    /// </summary>
    /// <param name="quantity"></param>
    public void SetItemQuantity(int quantity)
    {
        itemData.curStack = quantity;
        
        //数量等于1 就不显示文本
        _quantity.text = itemData.curStack > 1 ? quantity.ToString() : "";
    }

    public void SetItem(ItemData itemData, int quantity)
    {
        //添加数据
        this.itemData = itemData;
        //设置数量
        SetItemQuantity(quantity);

        //数量为0时销毁
        if(this.itemData == null || itemData.curStack == 0)
        {
            Debug.Log("销毁物品");
            Destroy(gameObject);
        }
    }
}
