using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    /// <summary>
    /// 树的最大生命值
    /// 表示可以砍伐的最大次数
    /// </summary>
    public int maxHealth;
    /// <summary>
    /// 树的当前生命值
    /// 表示剩余可以砍伐的次数
    /// </summary>
    public int currentHealth;

    private void Awake()
    {
        maxHealth = 5;
        currentHealth = maxHealth;
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
    /// 砍树
    /// 砍5次，获取5个树枝
    /// </summary>
    public void Chop()
    {
        currentHealth -= 1;

        if (currentHealth == 0)
        {
            ItemData treeItemData = DataManager.Instance.GetItemData(102);

            BackpackSystem.Instance.TryAddItem(treeItemData, 5);

            Destroy(gameObject);
        }
    }
}
