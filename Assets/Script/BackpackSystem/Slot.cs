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
}
