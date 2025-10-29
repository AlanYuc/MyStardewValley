using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 物品类
/// 处理所有拖拽事件以及保存物品数据
/// </summary>
public class Item : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler
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
        this.itemData.curStack = quantity;

        //数量等于1 就不显示文本
        _quantity.text = this.itemData.curStack > 1 ? quantity.ToString() : "";

        //数量为0时销毁
        if (this.itemData == null || this.itemData.curStack == 0)
        {
            Debug.Log("销毁物品");
            Destroy(gameObject);
        }
    }

    public void SetItem(ItemData itemData, int quantity)
    {
        //添加数据
        this.itemData = itemData;
        //AddItemData(itemData);

        //设置数量
        SetItemQuantity(quantity);

        //设置图片
        _icon.sprite = this.itemData.icon;
    }

    /// <summary>
    /// itemData的数据更新
    /// </summary>
    /// <param name="itemData"></param>
    private void AddItemData(ItemData itemData)
    {
        this.itemData.id = itemData.id;
        this.itemData.name = itemData.name;
        this.itemData.description = itemData.description;
        this.itemData.isStackable = itemData.isStackable;
        this.itemData.maxStack = itemData.maxStack;
        this.itemData.curStack = itemData.curStack;
        this.itemData.itemType = itemData.itemType;
        this.itemData.iconName = itemData.iconName;
        this.itemData.icon = itemData.icon;
        this.itemData.price = itemData.price;
    }

    /// <summary>
    /// 把item绑定到新的slot中
    /// 拿起和放下的通用方法
    /// </summary>
    /// <param name="targetSlot">目标slot</param>
    public void BindSlot(Slot targetSlot)
    {
        //将item移动到新的slot下
        transform.SetParent(targetSlot.transform);
        RectTransform rectTransform = transform as RectTransform;
        rectTransform.anchoredPosition = Vector2.zero;

        //重新双向绑定
        bindSlot = targetSlot;
        targetSlot.bindItem = this;
    }

    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //记录拖拽开始的格子
        BackpackSystem.Instance.lastSlot = bindSlot;

        Slot palmSlot = BackpackSystem.Instance.palm.slotList[0];

        if (BackpackSystem.Instance.palm.CheckNull())
        {
            //手里是空的，可以开始拖拽

            //断开旧绑定
            //先后顺序不可变
            bindSlot.bindItem = null;
            bindSlot = null;

            //设置父对象
            transform.SetParent(palmSlot.transform);

            //重新绑定
            bindSlot = palmSlot;
            bindSlot.bindItem = this;

            //BindSlot(palmSlot);
        }
    }

    /// <summary>
    /// 结束拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Slot focusSlot = BackpackSystem.Instance.focusSlot;
        Slot palmSlot = BackpackSystem.Instance.palm.slotList[0];

        if (focusSlot != null)
        {
            //鼠标停在格子上

            if (focusSlot.bindItem != null) 
            {
                //格子不是空的，需要判断类型，判断容量
                BackpackSystem.Instance.ExchangeOrMergeForDrag(palmSlot, focusSlot);
            }
            else
            {
                //格子是空的，直接放下
                BackpackSystem.Instance.PutAll(focusSlot);
            }

        }
        else
        {
            //鼠标没有停在格子上，那就将item扔在地上
            BackpackSystem.Instance.Discard(new ItemData(itemData));
            Destroy(gameObject);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
