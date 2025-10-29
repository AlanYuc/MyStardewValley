using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景内的可收集物体
/// </summary>
public class CollectItem : MonoBehaviour
{
    /// <summary>
    /// 物品信息
    /// </summary>
    public ItemData itemData;
    /// <summary>
    /// sprite组件
    /// </summary>
    public SpriteRenderer _spriteRenderer;
    /// <summary>
    /// 物品是否靠近玩家
    /// </summary>
    public bool isNearPlayer;
    /// <summary>
    /// 是否自动拾取
    /// </summary>
    public bool autoCollect;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = null;
        autoCollect = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isNearPlayer)
        {
            //在可拾取范围内按F拾取物品
            Debug.Log("拾取物品");

            Collect();
        }
    }

    /// <summary>
    /// 玩家进入拾取范围
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = true;

            if (autoCollect)
            {
                //玩家靠近，自动拾取
                Collect();
            }
        }
    }

    /// <summary>
    /// 玩家离开拾取范围
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }

    /// <summary>
    /// 拾取物品
    /// </summary>
    private void Collect()
    {
        //拾取到背包
        BackpackSystem.Instance.TryAddItem(itemData, itemData.curStack);

        //销毁
        Destroy(gameObject);
    }

    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="itemData"></param>
    /// <param name="quantity"></param>
    public void SetItemData(ItemData itemData, int quantity, bool isAutoCollect)
    {
        this.itemData = new ItemData(itemData);
        this.itemData.curStack = quantity;
        //将丢弃的item标记为不可自动拾取，防止出现 背包满-丢弃-自动拾取 的死循环
        this.autoCollect = isAutoCollect;
        _spriteRenderer.sprite = this.itemData.icon;
    }
}
