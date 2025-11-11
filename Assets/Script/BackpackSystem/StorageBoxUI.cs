using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 储物箱的背包界面
/// </summary>
public class StorageBoxUI : InventoryContainer
{
    // Start is called before the first frame update
    void Start()
    {
        //不在awake中执行，防止未获取canvas group的引用
        CloseContainer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// 展示储物箱界面
    /// </summary>
    /// <param name="itemDatas">储物箱中的所有物品</param>
    public void ShowStorageBox(List<ItemData> itemDatas)
    {
        //设置canvas group
        OpenContainer();

        //打开背包UI
        BackpackSystem.Instance.backpack.OpenContainer();

        //遍历数据，生成slot
        for (int i = 0; i < itemDatas.Count; i++)
        {
            if (itemDatas[i]!= null)
            {
                //在对应位置的slot添加数据
                slotList[i].AddItem(itemDatas[i], itemDatas[i].curStack);
            }
        }
    }

    /// <summary>
    /// 隐藏储物箱界面
    /// </summary>
    /// <returns>储物箱中剩下的物品</returns>
    public List<ItemData> CloseStorageBox()
    {
        //设置canvas group
        CloseContainer();

        //关闭背包UI
        BackpackSystem.Instance.backpack.CloseContainer();

        //统计储物箱的剩余数据
        List<ItemData> result = GetItemDataToList();

        //清空UI
        ClearAllSlot();

        return result;
    }
}
