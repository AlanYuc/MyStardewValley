using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景中的石头
/// </summary>
public class Rock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 采矿
    /// </summary>
    public void Mine()
    {
        //获取石头数据
        ItemData rockItemData = DataManager.Instance.GetItemData(100);

        //添加石头进入背包
        BackpackSystem.Instance.TryAddItem(rockItemData, 1);

        //销毁
        Destroy(gameObject);
    }
}
