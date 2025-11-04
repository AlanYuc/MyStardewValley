using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GridCellData
{
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
}
