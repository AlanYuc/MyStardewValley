using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 交易系统
/// </summary>
public class TradeSystem : MonoBehaviour
{
    public static TradeSystem Instance;
    /// <summary>
    /// 金币模块
    /// </summary>
    public CoinModule coinModule;

    private void Awake()
    {
        Instance = this;

        coinModule = GameObject.Find("CoinModule").GetComponent<CoinModule>();
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
