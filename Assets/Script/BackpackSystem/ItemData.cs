using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Tool,//工具
    Seed,//种子
    Material,//材料
    Food,//食物
    Medicine,//药水
    Weapon,//武器
    Special,//特殊
}

[Serializable]
public class ItemData
{
    /// <summary>
    /// 物品id
    /// </summary>
    public int id;
    /// <summary>
    /// 物品名称
    /// </summary>
    public string name;
    /// <summary>
    /// 物品描述
    /// </summary>
    public string description;
    /// <summary>
    /// 是否允许堆叠
    /// </summary>
    public bool isStackable;
    /// <summary>
    /// 允许堆叠的最大数量
    /// </summary>
    public int maxStack;
    /// <summary>
    /// 当前堆叠的数量
    /// </summary>
    public int curStack;
    /// <summary>
    /// 物品类型
    /// </summary>
    public ItemType itemType;
    /// <summary>
    /// 物品图标的名称
    /// </summary>
    public string iconName;
    /// <summary>
    /// 物品图标
    /// </summary>
    [JsonIgnore]
    public Sprite icon;
    /// <summary>
    /// 物品的购入价格
    /// </summary>
    public int price;

    public ItemData()
    {

    }
    
    public ItemData(ItemData itemData)
    {
        id = itemData.id;
        name = itemData.name;
        description = itemData.description;
        isStackable = itemData.isStackable;
        maxStack = itemData.maxStack;
        curStack = itemData.curStack;
        itemType = itemData.itemType;
        iconName = itemData.iconName;
        icon = itemData.icon;
        price = itemData.price;
    }
}
