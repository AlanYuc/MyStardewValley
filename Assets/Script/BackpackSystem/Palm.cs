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
        if (slotList[0].bindItem != null)
        {
            FollowMouse();
        }
    }

    /// <summary>
    /// item鼠标跟随
    /// </summary>
    private void FollowMouse()
    {
        //输出的坐标
        Vector2 canvasLocalPosition;

        //1.将鼠标位置转换为当前的canvas坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            UIManager.Instance._canvasRect,
            Input.mousePosition,
            UIManager.Instance.uiCamera,
            out canvasLocalPosition
            );

        //2.将canvas本地坐标转换为世界坐标
        Vector3 worldPosition = UIManager.Instance._canvas.transform.TransformPoint(canvasLocalPosition);

        //3.将世界坐标转换为父对象下的本地坐标
        Vector2 parentLocalPoint = slotList[0].transform.InverseTransformPoint(worldPosition);

        //4.平滑更新UI位置
        ((RectTransform)slotList[0].bindItem.transform).anchoredPosition = Vector2.Lerp(
            ((RectTransform)slotList[0].bindItem.transform).anchoredPosition,
            parentLocalPoint,
            Time.deltaTime * 200
            );
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

    /// <summary>
    /// 清空手上的物品
    /// </summary>
    public void ClearItem()
    {
        Destroy(slotList[0].bindItem.gameObject);
        slotList[0].bindItem = null;
    }
}
