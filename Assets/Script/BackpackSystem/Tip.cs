using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 物品提示
/// </summary>
public class Tip : MonoBehaviour
{
    /// <summary>
    /// 物品信息
    /// </summary>
    public ItemData itemData;
    /// <summary>
    /// 物品名称
    /// </summary>
    public TMP_Text _itemName;
    /// <summary>
    /// 物品内容
    /// </summary>
    public TMP_Text _itemContent;
    /// <summary>
    /// 是否显示tip
    /// </summary>
    public bool isShow;
    public CanvasGroup _canvasGroup;
    public RectTransform _rectTransform;
    /// <summary>
    /// 偏移量
    /// 移开tip，防止遮挡鼠标
    /// </summary>
    public Vector2 offset;

    private void Awake()
    {
        _canvasGroup    = GetComponent<CanvasGroup>();
        _rectTransform  = GetComponent<RectTransform>();
        _itemName       = transform.Find("TipTitle").GetComponent<TMP_Text>();
        _itemContent    = transform.Find("TipContent").GetComponent<TMP_Text>();

        offset = new Vector2(150, 40);
    }

    // Start is called before the first frame update
    void Start()
    {
        HideTip();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShow)
        {
            //显示的时候跟随鼠标
            FollowMouse();
        }
    }

    //鼠标跟随
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

        //增加偏移，防止遮挡鼠标射线检测
        canvasLocalPosition = canvasLocalPosition + offset;

        //2.平滑更新UI位置
        _rectTransform.anchoredPosition = Vector2.Lerp(
            _rectTransform.anchoredPosition,
            canvasLocalPosition,
            Time.deltaTime * 200
            );
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <param name="itemData"></param>
    public void SetItemData(ItemData itemData)
    {
        if (itemData != null)
        {
            _itemName.text = itemData.name.ToString();
            _itemContent.text = itemData.description.ToString();
        }
        else
        {
            _itemName.text = string.Empty;
            _itemContent.text = string.Empty;
        }
    }

    /// <summary>
    /// 显示tip
    /// </summary>
    public void ShowTip()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        isShow = true;
    }

    /// <summary>
    /// 隐藏tip
    /// </summary>
    public void HideTip()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        isShow = false;
    }
}
