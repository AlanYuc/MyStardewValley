using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背包的背包
/// </summary>
public class Backpack : InventoryContainer
{
    
    /// <summary>
    /// 整理背包 排序
    /// </summary>
    public Button _sortButton;

    private void Awake()
    {
        _sortButton = transform.Find("SortButton").GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CloseContainer();

        //添加事件
        _sortButton.onClick.AddListener(() =>
        {
            SortInventory();
        });
    }

    // Update is called once per frame
    void Update()
    {
        //按B打开和关闭背包
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!isOpen)
            {
                OpenContainer();
            }
            else
            {
                CloseContainer();
            }
        }
    }
}
