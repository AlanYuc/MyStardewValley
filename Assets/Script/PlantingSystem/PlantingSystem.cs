using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        Instance = this;

        toolModule = transform.Find("ToolModule").GetComponent<ToolModule>();
        seedModule = transform.Find("SeedModule").GetComponent<SeedModule>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
