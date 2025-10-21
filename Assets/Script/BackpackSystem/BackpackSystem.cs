using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSystem : MonoBehaviour
{
    /// <summary>
    /// 背包系统的单例
    /// </summary>
    public static BackpackSystem Instance;
    /// <summary>
    /// 背包的引用
    /// </summary>
    public Backpack backpack;
    /// <summary>
    /// 工具栏的引用
    /// </summary>
    public Toolbar toolbar;
    /// <summary>
    /// 插槽预制体
    /// </summary>
    public GameObject slot_prefab;

    private void Awake()
    {
        //初始化单例
        Instance = this;

        slot_prefab = Resources.Load<GameObject>("Prefab/Slot");

        //获取引用
        backpack = GetComponentInChildren<Backpack>();
        toolbar = GetComponentInChildren<Toolbar>();

        //初始化,背包和工具栏一行最多12个格子，该数值可以修改
        backpack.Initialize(12);
        toolbar.Initialize(12);
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
