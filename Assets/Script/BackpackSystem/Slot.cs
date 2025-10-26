using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 背包中的插槽脚本
/// </summary>
public class Slot : MonoBehaviour,IPointerClickHandler
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
    /// 添加item
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
        item.SetItem(itemData, quantity);
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
    }

    private void MiddleMouseClick()
    {
        
    }

    private void RightMouseClick()
    {
        
    }
}
