using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包容器的基类
/// </summary>
public class InventoryContainer : MonoBehaviour
{
    /// <summary>
    /// 所有插槽的列表
    /// </summary>
    public List<Slot> slotList = new List<Slot>();
    /// <summary>
    /// 插槽的父对象transform
    /// </summary>
    public Transform _slotListTrans;

    /// <summary>
    /// 背包初始化
    /// </summary>
    /// <param name="count">初始的物品数量</param>
    public void Initialize(int count)
    {
        _slotListTrans = transform.Find("SlotList");

        for (int i = 0; i < count; i++)
        {
            //实例化生成一个插槽，并将其slot组件信息添加到列表中
            GameObject go = Instantiate(BackpackSystem.Instance.slotPrefab, _slotListTrans);
            Slot slot = go.GetComponent<Slot>();
            slot.SetContainer(this);
            slotList.Add(slot);
        }
    }

    /// <summary>
    /// 在背包中查找可用的slot
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public Slot FindAvailableSlot(ItemData itemData)
    {
        foreach (Slot slot in slotList)
        {
            if (slot.bindItem != null &&
                slot.bindItem.itemData.id == itemData.id &&
                slot.bindItem.itemData.curStack < itemData.maxStack) 
            {
                return slot;
            }
        }
        return null;
    }

    /// <summary>
    /// 在特定背包中查找一个空的slot
    /// </summary>
    /// <returns></returns>
    public Slot FindEmptySlot()
    {
        foreach(Slot slot in slotList)
        {
            if(slot.bindItem == null)
            {
                return slot;
            }
        }

        return null;
    }
}
