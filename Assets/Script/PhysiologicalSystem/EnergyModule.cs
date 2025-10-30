using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 体力模块
/// </summary>
public class EnergyModule : MonoBehaviour
{
    /// <summary>
    /// 当前体力值
    /// </summary>
    public float currentEnergy;
    /// <summary>
    /// 体力值上限
    /// </summary>
    public float maxEnergy;

    /// <summary>
    /// 体力条
    /// </summary>
    public Slider _slider;

    private void Awake()
    {
        maxEnergy = 120f;
        currentEnergy = maxEnergy;

        _slider = GameObject.Find("EnergySlider").GetComponent<Slider>();
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
    /// 检查体力是否足够
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool CheckEnough(float amount)
    {
        return currentEnergy >= amount;
    }

    /// <summary>
    /// 消耗体力
    /// </summary>
    /// <param name="amount"></param>
    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;

        UpdateUI();
    }

    /// <summary>
    /// 设置体力
    /// </summary>
    /// <param name="energy"></param>
    public void SetEnergy(float energy)
    {
        currentEnergy = energy;

        UpdateUI();
    }

    /// <summary>
    /// 睡觉回复体力，10分钟调用一次
    /// </summary>
    /// <param name="sleepHours"></param>
    public void RestoreEnergyBySleeping(float sleepHours)
    {
        currentEnergy += sleepHours * 20;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

        UpdateUI();
    }

    /// <summary>
    /// 回复体力
    /// </summary>
    /// <param name="energy"></param>
    public void AddEnergy(float energy)
    {
        currentEnergy += energy;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

        UpdateUI();
    }

    /// <summary>
    /// 更新UI
    /// </summary>
    public void UpdateUI()
    {
        _slider.value = currentEnergy / maxEnergy;
    }
}
