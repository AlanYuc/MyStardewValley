using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 手掌的背包
/// 存放鼠标拖拽时拿在手里的物品，背包容量为1
/// </summary>
public class Palm : InventoryContainer
{
    /// <summary>
    /// 手掌背包单独初始化
    /// </summary>
    /// <param name="count"></param>
    public override void Initialize(int count)
    {
        //先调用虚方法的初始化
        base.Initialize(count);

        //关掉格子的背景
        Slot slot = slotList[0];
        slot.GetComponent<Image>().enabled = false;

        //关掉格子的所有交互
        CanvasGroup canvasGroup = slot.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
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
    /// 检测手掌背包的物品是否为空
    /// </summary>
    /// <returns></returns>
    public bool CheckNull()
    {
        if (slotList[0].bindItem == null)
        {
            return true;
        }
        return false;
    }
}
