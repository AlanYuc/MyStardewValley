using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GridCellData
{
    //土壤相关 实时更新

    /// <summary>
    /// 是否开垦
    /// </summary>
    public bool isPlowed = false;
    /// <summary>
    /// 是否浇水
    /// </summary>
    public bool isWatered = false;
    /// <summary>
    /// 是否种植
    /// </summary>
    public bool isPlanted = false;

    //种植相关 保存数据时更新

    /// <summary>
    /// 作物id
    /// </summary>
    public int plant_id = -1;
    /// <summary>
    /// 计时器
    /// </summary>
    public float timer = 0;
    /// <summary>
    /// 当前生长阶段
    /// </summary>
    public int current_state = 0;
    /// <summary>
    /// 是否可以生长
    /// </summary>
    public bool isGrowing = false;
    /// <summary>
    /// 是否成熟
    /// </summary>
    public bool isMature=false;
}
