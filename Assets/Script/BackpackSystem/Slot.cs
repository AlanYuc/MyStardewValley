using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 背包中的插槽脚本
/// </summary>
public class Slot : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    /// <summary>
    /// 插槽内对应的物品
    /// </summary>
    public Item bindItem;

    /// <summary>
    /// 该slot所属的父容器
    /// </summary>
    public InventoryContainer inventoryContainer;

    public void SetContainer(InventoryContainer ic)
    {
        inventoryContainer = ic;
    }

    /// <summary>
    /// 设置item数量
    /// </summary>
    /// <param name="quantity"></param>
    public void SetItemQuantity(int quantity)
    {
        bindItem.SetItemQuantity(quantity);
    }

    /// <summary>
    /// 想空的格子添加item
    /// </summary>
    /// <param name="itemData"></param>
    /// <param name="quantity"></param>
    public void AddItem(ItemData itemData, int quantity)
    {
        //生成item
        GameObject go = Instantiate(BackpackSystem.Instance.itemPrefab, transform);
        //获取item组件
        Item item = go.GetComponent<Item>();

        //双向绑定
        item.bindSlot = this;
        bindItem = item;

        //设置数据
        //使用new创建一个新的ItemDatas数据，否则会一直使用之前的itemData的引用
        item.SetItem(new ItemData(itemData), quantity);
    }

    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftMouseClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightMouseClick();
        }
        else if(eventData.button == PointerEventData.InputButton.Middle)
        {
            MiddleMouseClick();
        }
    }

    /// <summary>
    /// 左键点击
    /// </summary>
    private void LeftMouseClick()
    {
        if (BackpackSystem.Instance.palm.CheckNull())
        {
            //手掌是空的

            if(this.bindItem != null)
            {
                //点击的slot有物品

                //两个条件满足，可以拿起物品
                BackpackSystem.Instance.Pickup(this);
            }
        }
        else
        {
            if(this.bindItem == null)
            {
                //手掌不为空，且格子是空的，可以放下，且是全部放下
                BackpackSystem.Instance.PutAll(this);
            }
            else
            {
                //格子里有同类物品，有没有可用空间
                //格子里有非同类物品，进行交换

                BackpackSystem.Instance.ExchangeOrMergeForClick(
                    BackpackSystem.Instance.palm.slotList[0], this);
            }
        }
    }

    /// <summary>
    /// 中键点击
    /// </summary>
    private void MiddleMouseClick()
    {
        if(this.bindItem != null)
        {
            BackpackSystem.Instance.Use(this.bindItem);
        }
    }

    /// <summary>
    /// 右键点击
    /// </summary>
    private void RightMouseClick()
    {
        if (BackpackSystem.Instance.palm.CheckNull())
        {
            if(this.bindItem != null)
            {
                //手里空的，格子里有东西，直接拆分一半
                BackpackSystem.Instance.SplitStack(this.bindItem);
            }
        }
        else
        {
            //手里有东西，格子里无同类物品，无操作
            //手里有东西，格子里有同类物品，格子的物品已满，无操作
            //手里有东西，格子里有同类物品，格子的物品未满，放一个
            BackpackSystem.Instance.PutOne(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BackpackSystem.Instance.focusSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BackpackSystem.Instance.focusSlot = null;
    }
}
