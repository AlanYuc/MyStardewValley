using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
    public virtual void Initialize(int count)
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

    /// <summary>
    /// 容器排序
    /// </summary>
    public void SortInventory()
    {
        Dictionary<int, ItemData> items = new Dictionary<int, ItemData>();

        //1.统计所有物品id和总数
        foreach(Slot slot in slotList)
        {
            if (slot.bindItem != null)
            {
                int id = slot.bindItem.itemData.id;
                if (items.ContainsKey(id))
                {
                    //字典里的itemData是下面new出来的独立的
                    //因此直接用其中的curStack表示数量，不需要管maxStack
                    items[id].curStack += slot.bindItem.itemData.curStack;
                }
                else
                {
                    //new 一个新的itemData，防止互相影响
                    ItemData itemData = new ItemData(slot.bindItem.itemData);
                    items.Add(id, itemData);
                }
            }
        }

        //2.按照id大小排序，生成一个list
        //通过items.Keys获取包含所有id的列表
        //List<int> sortIDs = new List<int>(items.Keys);
        List<int> sortIDs = items.Keys.ToList();

        //从小到大排序
        sortIDs.Sort((a, b) => a.CompareTo(b));
        Debug.Log("排序列表中的id个数" + sortIDs.Count);
        Debug.Log("排序列表中的第一个id" + sortIDs[0]);

        //3.清空背包容器
        ClearAllSlot();

        //4.重新填充。遍历list，根据id查找字典，生成新的物体添加到背包
        foreach(int id in sortIDs)
        {
            int remainingAmount = items[id].curStack;//物品总数量
            int maxStack = items[id].maxStack;//最大堆叠数量

            while(remainingAmount >= maxStack)
            {
                //生成一个满的物品
                Slot slot = FindEmptySlot();
                slot.AddItem(items[id], maxStack);
                remainingAmount -= maxStack;
            }

            //还有剩余，再生成剩下的
            if (remainingAmount > 0)
            {
                Slot slot = FindEmptySlot();
                slot.AddItem(items[id], remainingAmount);
            }
        }
    }

    /// <summary>
    /// 清空所有格子
    /// </summary>
    private void ClearAllSlot()
    {
        Debug.Log("ClearAllSlot方法");

        foreach(Slot slot in slotList)
        {
            if(slot.bindItem != null)
            {
                Destroy(slot.bindItem.gameObject);
                slot.bindItem = null;
            }
        }
    }
}
