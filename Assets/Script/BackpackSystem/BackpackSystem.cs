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
    /// 手掌背包的引用
    /// </summary>
    public Palm palm;
    /// <summary>
    /// 插槽预制体
    /// </summary>
    public GameObject slotPrefab;
    /// <summary>
    /// 物品预制体
    /// </summary>
    public GameObject itemPrefab;
    /// <summary>
    /// 可拾取物品预制体
    /// </summary>
    public GameObject collectPrefab;
    /// <summary>
    /// 记录鼠标拖拽开始的slot
    /// </summary>
    public Slot lastSlot;
    /// <summary>
    /// 鼠标当前聚焦的格子
    /// </summary>
    public Slot focusSlot;

    private void Awake()
    {
        //初始化单例
        Instance = this;

        slotPrefab      = Resources.Load<GameObject>("Prefab/Slot");
        itemPrefab      = Resources.Load<GameObject>("Prefab/Item");
        collectPrefab   = Resources.Load<GameObject>("Prefab/CollectItem");

        //获取引用
        backpack = GetComponentInChildren<Backpack>();
        toolbar = GetComponentInChildren<Toolbar>();
        palm = GetComponentInChildren<Palm>();

        //初始化,背包和工具栏一行最多12个格子，手掌只有一个，该数值可以修改
        backpack.Initialize(12);
        toolbar.Initialize(12);
        palm.Initialize(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Test();
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

        if (Input.GetKeyDown(KeyCode.O))
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

        //4.查找背包是否由空格子
        while (surplus > 0)
        {
            //尝试找一个空格子
            Slot emptySlot = FindEmptySlot(backpack);

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

        //5.还有多余的生成在脚下
        if (surplus > 0)
        {
            ItemData surplusItemData = new ItemData(itemData);
            surplusItemData.curStack = surplus;
            Discard(surplusItemData);
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

    /// <summary>
    /// 拿起物品
    /// </summary>
    /// <param name="slot"></param>
    public void Pickup(Slot slot)
    {
        Debug.Log("Pickup方法");

        slot.bindItem.BindSlot(palm.slotList[0]);
        slot.bindItem = null;
    }

    /// <summary>
    /// 放下全部物品
    /// </summary>
    /// <param name="targetSlot">需要放进的格子</param>
    public void PutAll(Slot targetSlot)
    {
        Debug.Log("PutAll方法");

        palm.slotList[0].bindItem.BindSlot(targetSlot);
        palm.slotList[0].bindItem = null;
    }

    /// <summary>
    /// 使用物品
    /// </summary>
    /// <param name="item"></param>
    public void Use(Item item)
    {
        Debug.Log("Use方法");

        //To do
        Debug.Log("使用了物品");
    }

    /// <summary>
    /// 拆分一半
    /// </summary>
    /// <param name="item"></param>
    public void SplitStack(Item item)
    {
        Debug.Log("SplitStack方法");

        //拿起的item数量
        int pickupAmount = (int)MathF.Ceiling(item.itemData.curStack / 2.0f);

        //剩余的item数量
        int surplusAmount = item.itemData.curStack - pickupAmount;

        //手掌拿起物品
        palm.slotList[0].AddItem(new ItemData(item.itemData), pickupAmount);

        //更新原物品数量(顺序不可修改，需要在palm拿起物品后修改)
        item.SetItemQuantity(surplusAmount);
    }

    /// <summary>
    /// 放一个
    /// </summary>
    /// <param name="targetSlot"></param>
    public void PutOne(Slot targetSlot)
    {
        Debug.Log("PutOne方法");

        //获取引用
        Slot palmSlot = palm.slotList[0];

        if (targetSlot.bindItem != null)
        {
            if(targetSlot.bindItem.itemData.id == palmSlot.bindItem.itemData.id)
            {
                //相同物品，进行合并
                int availStack = targetSlot.bindItem.itemData.maxStack - targetSlot.bindItem.itemData.curStack;

                if(availStack > 0)
                {
                    //目标格子中的item数量+1
                    targetSlot.bindItem.SetItemQuantity(targetSlot.bindItem.itemData.curStack + 1);
                    //手上格子中的item数量-1
                    palmSlot.bindItem.SetItemQuantity(palmSlot.bindItem.itemData.curStack - 1);
                }
                else
                {
                    Debug.Log("目标位置物品已满，无法操作");
                }
            }
            else
            {
                //不同物品，无操作
                Debug.Log("目标位置有其他物品，无法操作");
            }
        }
        else
        {
            //目标位置是空的，可以直接放一个item
            //这里targetSlot.bindItem是空的，所以不能通过targetSlot.bindItem来获取itemData
            targetSlot.AddItem(palmSlot.bindItem.itemData, 1);
            palmSlot.bindItem.SetItemQuantity(palmSlot.bindItem.itemData.curStack - 1);
        }
    }

    /// <summary>
    /// 交换或合并item（点击）
    /// </summary>
    /// <param name="palmSlot"></param>
    /// <param name="targetSlot"></param>
    public void ExchangeOrMergeForClick(Slot palmSlot, Slot targetSlot)
    {
        Debug.Log("ExchangeOrMergeForClick方法");

        if(palmSlot.bindItem.itemData.id == targetSlot.bindItem.itemData.id)
        {
            //item的id相同
            //尝试合并
            MergeForClick(palmSlot, targetSlot);
        }
        else
        {
            //item的id不同
            //交换物品
            ExchangeForClick(palmSlot, targetSlot);
        }
    }

    /// <summary>
    /// 合并item（点击）
    /// </summary>
    /// <param name="palmSlot"></param>
    /// <param name="targetSlot"></param>
    private void MergeForClick(Slot palmSlot, Slot targetSlot)
    {
        //获取引用
        ItemData targetItemData = targetSlot.bindItem.itemData;
        ItemData palmItemData = palmSlot.bindItem.itemData;

        //获取可以合并的数量
        int availStack = targetItemData.maxStack - targetItemData.curStack;

        //判断是否可以合并
        if(availStack > 0)
        {
            Debug.Log("可以进行合并");

            //比较可合并数量和手掌的物品数量，判断手掌可以合并多少
            if(availStack >= palmItemData.curStack)
            {
                //手掌的物品可以全部合并

                int total = targetItemData.curStack + palmItemData.curStack;

                targetSlot.bindItem.SetItemQuantity(total);

                palm.ClearItem();
            }
            else
            {
                //手掌的物品只能合并部分，还有剩余

                int surplus = palmItemData.curStack - availStack;

                targetSlot.bindItem.SetItemQuantity(targetItemData.maxStack);

                palmSlot.bindItem.SetItemQuantity(surplus);
            }
        }
        else
        {
            Debug.Log("目标格子已满，无法合并");
        }
    }

    /// <summary>
    /// 交换item（点击）
    /// </summary>
    /// <param name="palmSlot"></param>
    /// <param name="targetSlot"></param>
    private void ExchangeForClick(Slot palmSlot, Slot targetSlot)
    {
        //备份palmSlot的item
        ItemData itemData = palmSlot.bindItem.itemData;

        //清空palmSlot的item
        Destroy(palmSlot.bindItem.gameObject);
        palmSlot.bindItem = null;

        //将targetSlot的item绑定到palmSlot下
        targetSlot.bindItem.BindSlot(palmSlot);

        //将备份的item绑定到targetSlot下
        targetSlot.AddItem(itemData, itemData.curStack);
    }

    /// <summary>
    /// 交换或合并item（拖拽）
    /// </summary>
    /// <param name="palmSlot"></param>
    /// <param name="targetSlot"></param>
    public void ExchangeOrMergeForDrag(Slot palmSlot, Slot targetSlot)
    {
        Debug.Log("ExchangeOrMergeForDrag方法");

        if (palmSlot.bindItem.itemData.id == targetSlot.bindItem.itemData.id)
        {
            //是同类，尝试合并

            ExchangeForDrag(palmSlot, targetSlot);
        }
        else
        {
            //不是同类，进行交换
            //拖拽的交换对象是拖拽起始和终点的两个格子，与点击交换有不同

            //把拖拽终点格子上的item放到起点格子里
            targetSlot.bindItem.BindSlot(lastSlot);
            targetSlot.bindItem = null;

            //把手上的item放到终点格子里，然后清空手里的
            palmSlot.bindItem.BindSlot(targetSlot);
            palmSlot.bindItem = null;
        }
    }

    /// <summary>
    /// 合并item（拖拽）
    /// </summary>
    /// <param name="palmSlot"></param>
    /// <param name="targetSlot"></param>
    private void ExchangeForDrag(Slot palmSlot, Slot targetSlot)
    {
        //获取引用
        ItemData palmItemData = palmSlot.bindItem.itemData;
        ItemData targetItemData = targetSlot.bindItem.itemData;

        //获取可以合并的数量
        int availStack = targetItemData.maxStack - targetItemData.curStack;

        if(availStack > 0)
        {
            //格子未满，可以合并
            Debug.Log("格子未满，可以合并");

            if(availStack >= palmItemData.curStack)
            {
                //可以全部合并

                int total = palmItemData.curStack + targetItemData.curStack;

                targetSlot.bindItem.SetItemQuantity(total);

                palm.ClearItem();
            }
            else
            {
                //只能合并部分

                int surplus = palmItemData.curStack - availStack;

                targetSlot.bindItem.SetItemQuantity(targetItemData.maxStack);

                //两种方法
                //1.先修改手上的数量，再放回起始格子
                //2.先清空手上的item，再在起始格子重新生成一个
                palm.ClearItem();
                lastSlot.AddItem(targetItemData, surplus);
            }
        }
        else
        {
            //格子已满，无法合并
            Debug.Log("格子已满，无法合并");

            //把手上的item放回去
            palmSlot.bindItem.BindSlot(lastSlot);
            palmSlot.bindItem = null;
        }
    }

    /// <summary>
    /// 丢弃物品，生成在玩家脚下
    /// </summary>
    /// <param name="itemData"></param>
    public CollectItem Discard(ItemData itemData)
    {
        return CreateItemInWolrd(Player.Instance.transform.position, itemData, itemData.curStack, false);
    }

    /// <summary>
    /// 在世界内生成item
    /// 除了从背包中丢弃时，NPC,宝箱等生成道具都会用到
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="itemData"></param>
    /// <param name="quantity"></param>
    public CollectItem CreateItemInWolrd(Vector3 pos,ItemData itemData,int quantity,bool isAutoCollect)
    {
        GameObject go = Instantiate(collectPrefab, pos, Quaternion.identity);
        CollectItem collectItem = go.GetComponent<CollectItem>();
        collectItem.SetItemData(itemData, quantity, isAutoCollect);

        return collectItem;
    }

    /// <summary>
    /// 永久删除物品
    /// </summary>
    public void DeleteItem()
    {
        //To do
    }
}
