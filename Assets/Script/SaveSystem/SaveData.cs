using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public PlayerSaveData playerSaveData;
    public PhysiologicalSaveData physiologicalSaveData;
    public TradeSaveData tradeSaveData;
    public TimeSaveData timeSaveData;
    public BackpackSaveData backpackSaveData;
    public PlantSaveData plantSaveData;
    public EnvironmentSaveData environmentSaveData;
}

/// <summary>
/// 供存档使用的玩家数据
/// </summary>
[Serializable]
public class PlayerSaveData
{
    /// <summary>
    /// 玩家位置x坐标
    /// </summary>
    public float player_pos_x;
    /// <summary>
    /// 玩家位置y坐标
    /// </summary>
    public float player_pos_y;
    /// <summary>
    /// 玩家位置z坐标
    /// </summary>
    public float player_pos_z;
    /// <summary>
    /// 玩家名称
    /// </summary>
    public string player_name;
    /// <summary>
    /// 农场名称
    /// </summary>
    public string farm_name;
    /// <summary>
    /// 玩家最喜欢的物品名称
    /// </summary>
    public string favorite;
    /// <summary>
    /// 经验值
    /// </summary>
    public int exp;
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
}

/// <summary>
/// 供存档使用的生理数据
/// </summary>
[Serializable]
public class PhysiologicalSaveData
{
    /// <summary>
    /// 玩家体力
    /// </summary>
    public float energy;
    /// <summary>
    /// 玩家血量
    /// </summary>
    public int hp;
}

/// <summary>
/// 供存档使用的贸易数据
/// </summary>
[Serializable]
public class TradeSaveData
{
    /// <summary>
    /// 玩家持有的金币数量
    /// </summary>
    public int coins;
}

/// <summary>
/// 供存档使用的时间数据
/// </summary>
[Serializable]
public class TimeSaveData
{
    public int currentYear;
    public Season currentSeason;
    public int currentDay;
    public int hour;
    public int minute;
    /// <summary>
    /// 计时器
    /// </summary>
    public float gameTimer;
    /// <summary>
    /// 游戏内时间
    /// </summary>
    public float gameTimeSecond;
    /// <summary>
    /// 真实时间
    /// </summary>
    public float realTimeSecond;
}

/// <summary>
/// 供存档使用的背包数据
/// </summary>
[Serializable]
public class BackpackSaveData
{
    /// <summary>
    /// 当前激活的格子，红框的位置
    /// </summary>
    public int active_slot;
    /// <summary>
    /// 工具栏物品列表
    /// </summary>
    public List<ItemData> toolbar_item_list = new List<ItemData>();
    /// <summary>
    /// 背包物品列表
    /// </summary>
    public List<ItemData> backpack_item_list = new List<ItemData>();
    /// <summary>
    /// 丢在地上的物品列表
    /// </summary>
    public List<DiscardItemData> pickup_items_list = new List<DiscardItemData>();
    /// <summary>
    /// 储物箱物品列表
    /// </summary>
    public List<BoxData> box_data_list = new List<BoxData>();
}

/// <summary>
/// 供存档使用的种植数据
/// </summary>
[Serializable]
public class PlantSaveData
{
    /// <summary>
    /// 二维网格，每个网格都有存储对应土壤的种植信息
    /// </summary>
    public GridCellData[,] gridData;
}

/// <summary>
/// 供存档使用的环境数据
/// </summary>
[Serializable]
public class EnvironmentSaveData
{
    /// <summary>
    /// 场景内的环境物体
    /// 保存的是物品id，以及该id的所有物品
    /// </summary>
    public Dictionary<int, List<EasyData>> stateless_objects = new Dictionary<int, List<EasyData>>();
}

/// <summary>
/// 简单数据，目前只记录Transform
/// </summary>
[Serializable]
public class EasyData
{
    public float x;
    public float y;
    public float z;

    //rotate
    //scale
}

/// <summary>
/// 丢弃物品类
/// </summary>
[Serializable]
public class DiscardItemData
{
    //丢弃物品的世界坐标
    public float x;
    public float y;
    public float z;
    public ItemData item_data = new ItemData();
}

/// <summary>
/// 储物箱物品类
/// </summary>
[Serializable]
public class BoxData
{
    public float x;
    public float y;
    public float z;
    public List<ItemData> item_datas = new List<ItemData>();
}