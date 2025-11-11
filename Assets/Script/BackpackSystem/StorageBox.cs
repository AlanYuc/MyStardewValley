using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 储物箱
/// </summary>
public class StorageBox : MonoBehaviour
{
    /// <summary>
    /// 玩家是否靠近
    /// </summary>
    public bool isNearPlayer;
    /// <summary>
    /// 储物箱是否打开
    /// </summary>
    public bool isOpenBox;
    /// <summary>
    /// 储物箱中的物品数据
    /// </summary>
    public List<ItemData> itemDataList = new List<ItemData>();

    private void Awake()
    {
        isNearPlayer = false;
        isOpenBox = false;

        //初始化itemDataList,储物箱的格子数固定为36
        if (itemDataList.Count == 0)
        {
            for(int i = 0; i < 36; i++)
            {
                itemDataList.Add(null);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //打开，关闭储物箱
        if(isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpenBox)
            {
                //关闭储物箱
                isOpenBox = false;

                //关闭UI
                itemDataList = BackpackSystem.Instance.storageBox.CloseStorageBox();
            }
            else
            {
                //打开储物箱
                isOpenBox = true;

                //打开UI
                BackpackSystem.Instance.storageBox.ShowStorageBox(itemDataList);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }
}
