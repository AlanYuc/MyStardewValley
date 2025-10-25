using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 金币模块
/// </summary>
public class CoinModule : MonoBehaviour
{
    /// <summary>
    /// 金币的文本
    /// </summary>
    public TMP_Text _coinTxt;
    /// <summary>
    /// 当前金币数量
    /// </summary>
    public int currentCoin;

    private void Awake()
    {
        _coinTxt = GameObject.Find("PlayerMoney").GetComponent<TMP_Text>();
        currentCoin = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 设置金币数量
    /// </summary>
    /// <param name="coin"></param>
    public void SetCoin(int coin)
    {
        currentCoin = coin;
        UpdateUI();
    }

    /// <summary>
    /// 修改金币
    /// </summary>
    /// <param name="cost"></param>
    public void ChangeCoin(int cost)
    {
        currentCoin += cost;
        UpdateUI();
    }

    /// <summary>
    /// 更新金币UI
    /// </summary>
    public void UpdateUI()
    {
        _coinTxt.text = currentCoin.ToString();
    }

    /// <summary>
    /// 检测金币是否够用
    /// </summary>
    /// <param name="cost"></param>
    /// <returns></returns>
    public bool CheckEnough(int cost)
    {
        return currentCoin - cost > 0;
    }
}
