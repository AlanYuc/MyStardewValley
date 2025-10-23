using System;
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
    public GameObject slotPrefab;
    /// <summary>
    /// 物品预制体
    /// </summary>
    public GameObject itemPrefab;

    private void Awake()
    {
        //初始化单例
        Instance = this;

        slotPrefab = Resources.Load<GameObject>("Prefab/Slot");
        itemPrefab = Resources.Load<GameObject>("Prefab/Item");

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
        Test();
    }

    /// <summary>
    /// 测试
    /// </summary>
    public void Test()
    {
        //模拟从商店购物，然后添加一个道具进入背包
        if (Input.GetKeyDown(KeyCode.P))
        {
            TryAddItem(DataManager.Instance.itemDataList[0],1);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            TryAddItem(DataManager.Instance.itemDataList[8], 1);
        }
    }

    public void TryAddItem(ItemData itemData, int quantity)
    {
        //剩余未被添加的道具数量
        int surplus = quantity;

        //1.查找工具栏是否有同类item
        while (surplus > 0) 
        {
            //尝试在工具栏获取一个可用的格子
            Slot slot = FindAvailableSlot(toolbar, itemData);

            //slot为空说明工具栏没有可用的格子
            if(slot == null)
            {
                break;
            }

            //获取溢出值，看是否超出对应物品堆叠的最大数量
            int overflow = slot.bindItem.itemData.curStack + surplus - itemData.maxStack;

            if (overflow > 0)
            {
                //超出上限
                //将item数量设置为最大值
                slot.SetItemQuantity(itemData.maxStack);
            }
            else
            {
                //未超出上限
                //直接设置item数量
                slot.SetItemQuantity(slot.bindItem.itemData.curStack + surplus);
            }

            surplus = overflow;
        }

        //2.查找背包是否有同类item
        while (surplus > 0)
        {
            //尝试在工具栏获取一个可用的格子
            Slot slot = FindAvailableSlot(backpack, itemData);

            //slot为空说明工具栏没有可用的格子
            if (slot == null)
            {
                break;
            }

            //获取溢出值，看是否超出对应物品堆叠的最大数量
            int overflow = slot.bindItem.itemData.curStack + surplus - itemData.maxStack;

            if (overflow > 0)
            {
                //超出上限
                //将item数量设置为最大值
                slot.SetItemQuantity(itemData.maxStack);
            }
            else
            {
                //未超出上限
                //直接设置item数量
                slot.SetItemQuantity(slot.bindItem.itemData.curStack + surplus);
            }

            surplus = overflow;
        }

        //3.查找工具栏是否有空格子
        while (surplus > 0)
        {
            //尝试找一个空格子
            Slot emptySlot = FindEmptySlot(toolbar);

            //没有空格子
            if (emptySlot == null)
            {
                break;
            }

            //溢出值
            int overflow = surplus - itemData.maxStack;
            if (overflow > 0)
            {
                emptySlot.AddItem(itemData, itemData.maxStack);
            }
            else
            {
                emptySlot.AddItem(itemData, surplus);
            }
            surplus = overflow;
        }

        
    }

    /// <summary>
    /// 在特定背包中查找一个空的slot
    /// </summary>
    /// <param name="container"></param>
    /// <returns></returns>
    public Slot FindEmptySlot(InventoryContainer container)
    {
        return container.FindEmptySlot();
    }

    /// <summary>
    /// 在特定的背包中查找一个可用的slot
    /// </summary>
    /// <param name="container"></param>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public Slot FindAvailableSlot(InventoryContainer container, ItemData itemData)
    {
        return container.FindAvailableSlot(itemData);
    }
}
