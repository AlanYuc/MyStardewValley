using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    /// <summary>
    /// 空
    /// </summary>
    None,
    /// <summary>
    /// 锄头->耕地
    /// </summary>
    Hoe,
    /// <summary>
    /// 斧头->砍树
    /// </summary>
    Axe,
    /// <summary>
    /// 镐子->采石头
    /// </summary>
    Pickaxe,
    /// <summary>
    /// 镰刀->割草
    /// </summary>
    Scythe,
    /// <summary>
    /// 浇水壶
    /// </summary>
    WateringCan,
    /// <summary>
    /// 钓鱼竿
    /// </summary>
    FishingRod, 
}

public class ToolModule : MonoBehaviour
{
    /// <summary>
    /// 当前的工具
    /// </summary>
    public ToolType currentTool;
    /// <summary>
    /// 工具的使用范围
    /// </summary>
    public float useRange;
    /// <summary>
    /// 体力值消耗
    /// (割草不消耗)
    /// </summary>
    public int energyCost;

    /// <summary>
    /// 当前浇水壶的容量
    /// </summary>
    public int currentWateringCanCapacity;
    /// <summary>
    /// 浇水壶的最大容量
    /// 10表示可以浇水10次
    /// </summary>
    public int maxWateringCanCapacity;
    /// <summary>
    /// 玩家是否靠近水源
    /// </summary>
    public bool isNearWater = false;

    public EnergyModule energyModule;

    private void Awake()
    {
        maxWateringCanCapacity = 10;
        currentWateringCanCapacity = maxWateringCanCapacity;
        energyCost = 5;
        useRange = 0.4f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //不在awake中获取，防止energyModule为空
        energyModule = PhysiologicalSystem.Instance.energyModule;
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
        //检查拿的是否是镰刀，不是镰刀再检查体力
        if (currentTool != ToolType.Scythe && !energyModule.CheckEnough(energyCost)) 
        {
            
            Debug.Log("体力不够，无法使用");
            return;
        }

        //检查浇水壶，看水是否够用
        if(currentTool == ToolType.WateringCan && currentWateringCanCapacity == 0)
        {
            Debug.Log("水不够了，无法使用");
            return;
        }

        //开始行动
        PerformToolAction();
    }

    /// <summary>
    /// 使用工具
    /// </summary>
    private void PerformToolAction()
    {
        //射线检测
        RaycastHit2D[] hits = Physics2D.RaycastAll(
            Player.Instance.transform.position,
            GetMouseDirection(),
            useRange //在限制范围内
            );

        //排除射线中检测到的玩家
        RaycastHit2D hit = default;
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider.CompareTag("Player"))
            {
                continue;
            }
            hit = h;
        }

        if(hit.collider == null)
        {
            return;
        }

        switch (currentTool)
        {
            case ToolType.None:
                break;
            case ToolType.Hoe:
                break;
            case ToolType.Axe:
                break;
            case ToolType.Pickaxe:
                if (hit.collider.CompareTag("Rock"))
                {
                    hit.collider.GetComponent<Rock>().Mine();
                    ConsumeEnergy();
                }
                break;
            case ToolType.Scythe:
                break;
            case ToolType.WateringCan:
                break;
            case ToolType.FishingRod:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 消耗体力
    /// </summary>
    private void ConsumeEnergy()
    {
        energyModule.UseEnergy(energyCost);
    }

    /// <summary>
    /// 获取鼠标相对玩家的方向方向
    /// </summary>
    /// <returns></returns>
    private Vector2 GetMouseDirection()
    {
        //鼠标的世界坐标
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //玩家的世界坐标
        Vector3 playerPos = Player.Instance.transform.position;

        Vector3 mouseDir = (mousePos - playerPos).normalized;

        return mouseDir;
    }

    public void UpdateItem(Item item)
    {
        //当前的格子没有东西
        if(item == null)
        {
            currentTool = ToolType.None;
            return;
        }

        //判断传过来的物品名称，只有是工具的时候进行记录
        //名称需要与itemData中的名称一致
        switch (item.itemData.name)
        {
            case "锄头":
                currentTool = ToolType.Hoe;
                break;
            case "镐":
                currentTool = ToolType.Pickaxe;
                break;
            case "斧子":
                currentTool = ToolType.Axe;
                break;
            case "镰刀":
                currentTool = ToolType.Scythe;
                break;
            case "花洒":
                currentTool = ToolType.WateringCan;
                break;
            case "鱼竿":
                currentTool = ToolType.FishingRod;
                break;
            default:
                currentTool = ToolType.None;
                break;
        }

        Debug.Log("当前工具是：" + currentTool);
    }
}
