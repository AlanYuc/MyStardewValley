using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工具栏的背包
/// </summary>
public class Toolbar : InventoryContainer
{
    /// <summary>
    /// 当前选中的工具栏格子的索引
    /// </summary>
    public int selectSlot;
    /// <summary>
    /// ActiveSlot的RectTransform组件
    /// </summary>
    public RectTransform _activeSlotRectTransform;
    /// <summary>
    /// 红框的初始位置
    /// </summary>
    public Vector2 activeSlotOriginPos;
    /// <summary>
    /// 红框之间的间距
    /// </summary>
    public float slotSpacing;

    /// <summary>
    /// 工具栏按键列表
    /// </summary>
    public List<KeyCode> keyList = new List<KeyCode>();

    private void Awake()
    {
        _activeSlotRectTransform = transform.Find("ActiveSlot").GetComponent<RectTransform>();

        selectSlot = 0;
        activeSlotOriginPos = _activeSlotRectTransform.anchoredPosition;
        slotSpacing = 64;

        AddKey();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //监听按键 切换工具栏格子
        for(int i= 0; i < keyList.Count; i++)
        {
            if (Input.GetKeyDown(keyList[i]))
            {
                SwitchSlot(i);
            }
        }
    }

    private void AddKey()
    {
        keyList.Add(KeyCode.Alpha1);
        keyList.Add(KeyCode.Alpha2);
        keyList.Add(KeyCode.Alpha3);
        keyList.Add(KeyCode.Alpha4);
        keyList.Add(KeyCode.Alpha5);
        keyList.Add(KeyCode.Alpha6);
        keyList.Add(KeyCode.Alpha7);
        keyList.Add(KeyCode.Alpha8);
        keyList.Add(KeyCode.Alpha9);
        keyList.Add(KeyCode.Alpha0);
        keyList.Add(KeyCode.Minus);//键盘 -
        keyList.Add(KeyCode.Equals);//键盘 +/=
    }

    //切换格子
    public void SwitchSlot(int indexSlot)
    {
        //更新索引记录
        selectSlot = indexSlot;

        //更新UI 红框的位置
        _activeSlotRectTransform.anchoredPosition = activeSlotOriginPos + new Vector2(selectSlot * slotSpacing, 0);

        //获取红框内的item
        Item item = slotList[selectSlot].bindItem;

        //切换item，通知各个模块(工具模块，种子模块等)
        SwitchItem(item);
    }

    /// <summary>
    /// 通知其他模块，切换到当前物品
    /// </summary>
    /// <param name="item"></param>
    private void SwitchItem(Item item)
    {
        //工具模块 获取工具模块中选中的物品
        PlantingSystem.Instance.toolModule.UpdateItem(item);

        //种子模块
        PlantingSystem.Instance.seedModule.UpdateItem(item);

        //食物
        //武器
        //..
    }
}
