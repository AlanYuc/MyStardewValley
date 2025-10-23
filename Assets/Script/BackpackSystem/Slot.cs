using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包中的插槽脚本
/// </summary>
public class Slot : MonoBehaviour
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

        Debug.Log("生成一个物品后的数量：" + item.itemData.curStack);
        Debug.Log("传递的进来的数量：" + itemData.curStack);

        //设置数据
        item.SetItem(itemData, quantity);
    }
}
