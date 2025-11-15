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
public class TimeSaveData
{
    public int currentYear;
    public Season currentSeason;
    public int currentDay;
    public int hour;
    public int minute;
    public float gameTimer;
    public float gameTimeSecond;
    public float realTimeSecond;
}

/// <summary>
/// 供存档使用的背包数据
/// </summary>
public class BackpackSaveData
{
    public int active_slot;
}

/// <summary>
/// 供存档使用的种植数据
/// </summary>
public class PlantSaveData
{

}

/// <summary>
/// 供存档使用的环境数据
/// </summary>
public class EnvironmentSaveData
{

}