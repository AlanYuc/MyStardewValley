using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品类
/// 处理所有拖拽事件以及保存物品数据
/// </summary>
public class Item : MonoBehaviour
{
    /// <summary>
    /// 物品数据
    /// </summary>
    public ItemData itemData;
    /// <summary>
    /// 物品的图标
    /// </summary>
    public Image _icon;
    /// <summary>
    /// 物品的数量
    /// </summary>
    public TMP_Text _quantity;

    /// <summary>
    /// 物品所在的插槽
    /// </summary>
    public Slot bindSlot;

    private void Awake()
    {
        _icon       = transform.Find("Icon").GetComponent<Image>();
        _quantity   = transform.Find("Quantity").GetComponent<TMP_Text>();

        _icon.sprite = null;
        _quantity.text = "";
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
