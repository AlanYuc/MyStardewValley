using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 种子类型
/// </summary>
public enum SeedType
{
    None,
    /// <summary>
    /// 防风草(欧洲萝卜)
    /// </summary>
    Parsnip,
    /// <summary>
    /// 南瓜
    /// </summary>
    Pumpkin,
    /// <summary>
    /// 辣椒
    /// </summary>
    Pepper,
    /// <summary>
    /// 白萝卜
    /// </summary>
    Turnip,
}

public class SeedModule : MonoBehaviour
{
    public SeedType currentSeed;

    private void Awake()
    {
        currentSeed = SeedType.None;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(currentSeed == SeedType.None)
            {
                return;
            }

            TrySowSeed();
        }
    }

    /// <summary>
    /// 尝试进行播种
    /// </summary>
    public void TrySowSeed()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        PlantingSystem.Instance.TrySowSeed(mouseWorldPos);
    }

    /// <summary>
    /// 接收工具栏中选中的物品
    /// </summary>
    /// <param name="item"></param>
    public void UpdateItem(Item item)
    {
        //当前的格子没有东西
        if (item == null)
        {
            currentSeed = SeedType.None;
            return;
        }

        //不是种子
        if(item.itemData.itemType != ItemType.Seed)
        {
            currentSeed = SeedType.None;
            return;
        }

        switch (item.itemData.name)
        {
            case "防风草种子":
                currentSeed = SeedType.Parsnip;
                break;
            case "辣椒种子":
                currentSeed = SeedType.Pepper;
                break;
            case "南瓜种子":
                currentSeed = SeedType.Pumpkin;
                break;
            case "白萝卜种子":
                currentSeed = SeedType.Turnip;
                break;
            default:
                currentSeed = SeedType.None;
                break;
        }
    }
}
