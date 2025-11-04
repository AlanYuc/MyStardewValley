using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// 种植系统
/// </summary>
public class PlantingSystem : MonoBehaviour
{
    public static PlantingSystem Instance;

    /// <summary>
    /// 工具模块
    /// </summary>
    public ToolModule toolModule;
    /// <summary>
    /// 种子模块
    /// </summary>
    public SeedModule seedModule;

    /// <summary>
    /// 开垦土地的网格数据
    /// </summary>
    public GridCellData[,] gridCellDatas;
    /// <summary>
    /// 已激活的土壤地块的字典
    /// </summary>
    public Dictionary<Vector2Int,SoilPlot> activeSoilPlots = new Dictionary<Vector2Int,SoilPlot>();
    /// <summary>
    /// 开垦土地的网格大小
    /// Vector2Int(5, 5)表示5*5的网格
    /// </summary>
    public Vector2Int gridSize;
    /// <summary>
    /// 土地网格的偏移量
    /// </summary>
    public Vector2 gridOffset;
    /// <summary>
    /// 一个网格的大小
    /// </summary>
    public float cellSize;
    /// <summary>
    /// 土地网格的父对象
    /// </summary>
    public Transform _soilGridTrans;
    /// <summary>
    /// 土地的prefab
    /// </summary>
    public GameObject soilPlotPrefab;

    private void Awake()
    {
        Instance = this;

        toolModule = transform.Find("ToolModule").GetComponent<ToolModule>();
        seedModule = transform.Find("SeedModule").GetComponent<SeedModule>();

        _soilGridTrans = GameObject.Find("SoilGrid").transform;

        soilPlotPrefab = Resources.Load<GameObject>("Prefab/SoilPlot");

        gridSize = new Vector2Int(5, 5);
        gridOffset = new Vector2(-1.6f, 1.7f);
        cellSize = 0.4f;
        InitGrid();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 初始化网格数据GridCellData
    /// </summary>
    private void InitGrid()
    {
        //初始化二维数组
        gridCellDatas = new GridCellData[gridSize.x, gridSize.y];

        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                //初始化单个GridCellData数据
                gridCellDatas[x, y] = new GridCellData();
            }
        }
    }

    /// <summary>
    /// 创建土壤地块
    /// </summary>
    public SoilPlot CreateSoilPlot(Vector3 mousePos)
    {
        Debug.Log("CreateSoilPlot");

        //将鼠标的世界坐标转换成网格坐标
        Vector2Int gridPos = WorldToGrid(mousePos);

        //判断鼠标位置是否在可开垦土地上
        if (!IsPositionValid(gridPos))
        {
            return null;
        }

        //防止同一块土壤二次生成（字典也会报错）
        if (activeSoilPlots.ContainsKey(gridPos))
        {
            return null;
        }

        //再转换回世界坐标，就是土壤地块的生成坐标
        Vector3 spawnPos = new Vector3(
            gridPos.x * cellSize + cellSize / 2 + gridOffset.x,
            gridPos.y * cellSize + cellSize / 2 + gridOffset.y,
            0
            );

        //生成土壤
        GameObject go = Instantiate(soilPlotPrefab, spawnPos, Quaternion.identity, _soilGridTrans);
        SoilPlot soilPlot = go.GetComponent<SoilPlot>();

        //设置土壤状态
        gridCellDatas[gridPos.x, gridPos.y].isPlowed = true;
        gridCellDatas[gridPos.x, gridPos.y].isWatered = false;
        gridCellDatas[gridPos.x, gridPos.y].isPlanted = false;

        //记录已激活的土壤
        activeSoilPlots.Add(gridPos, soilPlot);

        return soilPlot;
    }

    /// <summary>
    /// 判断鼠标位置是否在可开垦土地上
    /// </summary>
    /// <param name="gridPos"></param>
    public bool IsPositionValid(Vector2Int gridPos)
    {
        if (gridPos.x >= 0 && gridPos.x < gridSize.x && 
            gridPos.y >= 0 && gridPos.y < gridSize.y) 
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 将世界坐标转成网格内的坐标
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns>网格二维坐标</returns>
    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt((worldPos.x - gridOffset.x) / cellSize);
        int y = Mathf.FloorToInt((worldPos.y - gridOffset.y) / cellSize);

        Debug.Log("网格坐标:" + new Vector2Int(x, y));
        return new Vector2Int(x, y);
    }
}
