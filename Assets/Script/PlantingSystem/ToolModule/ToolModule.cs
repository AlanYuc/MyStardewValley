using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    None, //空

    Hoe, //锄头->耕地
    Axe, //斧头->砍树
    Pickaxe, //镐子->采石头
    Scythe, //镰刀->割草
    WateringCan, //浇水壶
    FishingRod, //钓鱼竿
}

public class ToolModule : MonoBehaviour
{
    /// <summary>
    /// 当前的工具
    /// </summary>
    public ToolType currentTool;
    /// <summary>
    /// 工具的食用范围
    /// </summary>
    public float useRange = 0.4f;
    /// <summary>
    /// 体力值消耗
    /// (割草不消耗)
    /// </summary>
    public int energyCost = 5;

    /// <summary>
    /// 当前浇水壶的容量
    /// </summary>
    public int currentWateringCanCapacity;
    /// <summary>
    /// 浇水壶的最大容量
    /// 10表示可以浇水10次
    /// </summary>
    public int maxWateringCanCapacity = 10;
    /// <summary>
    /// 玩家是否靠近水源
    /// </summary>
    public bool isNearWater = false;

    private void Awake()
    {
        currentWateringCanCapacity = maxWateringCanCapacity;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //没有工具，不处理
        if(currentTool == ToolType.None)
        {
            return;
        }

        //有工具，且按下了左键
        if (Input.GetMouseButtonDown(0))
        {
            //尝试使用工具
            TryUseTool();
        }
    }

    /// <summary>
    /// 尝试使用工具
    /// </summary>
    private void TryUseTool()
    {
        
    }
}
