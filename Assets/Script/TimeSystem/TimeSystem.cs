using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

/// <summary>
/// 季节
/// </summary>
public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}

/// <summary>
/// 时间系统
/// </summary>
public class TimeSystem : MonoBehaviour
{
    public static TimeSystem Instance;

    //基本设置

    /// <summary>
    /// 游戏的一天在现实的秒数
    /// </summary>
    public float realSecondPerDay;
    /// <summary>
    /// 游戏内一天的开始时间（角色起床时间）
    /// </summary>
    public int dayStartHour;
    /// <summary>
    /// 强制开始睡眠时间
    /// </summary>
    public int forcedSleepHour;
    /// <summary>
    /// 游戏内一个季度多少天
    /// </summary>
    public int dayPerSeason;
    /// <summary>
    /// 动态时间比例尺
    /// 玩家睡觉时进行时间加速
    /// </summary>
    public float timeScale;
    /// <summary>
    /// 静态时间比例尺
    /// 正常游戏时的游戏内时间流速
    /// </summary>
    public float staticTimeScale;
    /// <summary>
    /// 夜晚睡觉后时间加速的倍率
    /// </summary>
    public float nightTimeMultiplier;

    //动态修改

    /// <summary>
    /// 当前季节
    /// </summary>
    public Season currentSeason;
    /// <summary>
    /// 当前年
    /// </summary>
    public int currentYear;
    /// <summary>
    /// 当前天
    /// </summary>
    public int currentDay;
    /// <summary>
    /// 当前小时
    /// </summary>
    public int currentHour;
    /// <summary>
    /// 当前分钟
    /// </summary>
    public int currentMinute;
    /// <summary>
    /// 当前是星期几
    /// </summary>
    public int weekday;

    /// <summary>
    /// 当前存档的累计游玩时长(现实时间)
    /// </summary>
    public float realTimeAccumulator;
    /// <summary>
    /// 当前存档的游戏内的累计时间
    /// </summary>
    public float virtualAccumulator;
    /// <summary>
    /// 游戏内定时器
    /// </summary>
    public float gameTimer;


    /// <summary>
    /// 玩家是否正在睡觉
    /// </summary>
    public bool isSleeping;
    /// <summary>
    /// 时间的UI文本
    /// </summary>
    public TMP_Text _timeText;
    /// <summary>
    /// 日期的UI文本
    /// </summary>
    public TMP_Text _dateText;

    public EnergyModule energyModule;

    private void Awake()
    {
        Instance = this;

        realSecondPerDay = 60;//游戏一天为60s
        dayStartHour = 6;//游戏一天从6点开始，起床
        forcedSleepHour = 2;//凌晨2点强制睡觉
        dayPerSeason = 30;//游戏内30天为一个季度
        nightTimeMultiplier = 20;//夜晚时间加速倍率为20

        currentSeason = Season.Spring;
        currentYear = 0;
        currentDay = 1;
        currentHour = dayStartHour;//初始从早上6点开始
        currentMinute = 0;
        weekday = currentDay;//从星期一开始

        //初始化比例尺 现实一天的秒数/游戏内一天的秒数
        timeScale = 24 * 60 * 60 / realSecondPerDay;
        staticTimeScale = timeScale;

        _dateText = GameObject.Find("Date").GetComponent<TMP_Text>();
        _timeText = GameObject.Find("Time").GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        energyModule = GameObject.Find("EnergyModule").GetComponent<EnergyModule>();

        //当前时间 00:00
        _timeText.text = currentHour.ToString("D2") + ":" + currentMinute.ToString("D2");
        //当前天数
        _dateText.text = currentDay.ToString() + "日 " + "星期" + weekday;
    }

    // Update is called once per frame
    void Update()
    {
        realTimeAccumulator += Time.deltaTime;//累计现实时间
        virtualAccumulator += Time.deltaTime * timeScale;//累计游戏内时间
        gameTimer += Time.deltaTime * timeScale;//游戏内时间定时器

        //游戏内时间每十分钟更新一次UI
        if(gameTimer >= 60 * 10)
        {
            gameTimer = 0;

            UpdateGameTime();//更新游戏内时间和UI

            //更新时间的时候检测一次，降低检测频率
            //检测睡觉的情况
            if (!isSleeping)
            {
                //没睡觉时，检测是否强制睡觉
                CheckForcedSleep();
            }
            else
            {
                //睡觉的时候恢复体力
                RecoverEnergy();

                //睡觉时，检测是否起床
                CheckForcedWakeUp();
            }
        }

        //按键L模拟上床睡觉
        if (Input.GetKeyDown(KeyCode.L))
        {
            ManualSleep();
        }
    }

    /// <summary>
    /// 恢复体力
    /// </summary>
    private void RecoverEnergy()
    {
        energyModule.RestoreEnergyBySleeping(10f / 60f);
    }

    /// <summary>
    /// 开始睡觉
    /// </summary>
    private void ManualSleep()
    {
        ForceSleep();
    }

    /// <summary>
    /// 检测是否起床
    /// </summary>
    private void CheckForcedWakeUp()
    {
        //凌晨六点整起床
        if (currentHour == dayStartHour && currentMinute == 0)
        {
            ForceWakeUp();
        }
    }

    /// <summary>
    /// 强制角色起床
    /// </summary>
    private void ForceWakeUp()
    {
        isSleeping = false;

        //复原时间比例尺
        timeScale = staticTimeScale;//白天的时间不加速
    }

    /// <summary>
    /// 检测是否需要强制角色睡觉
    /// </summary>
    private void CheckForcedSleep()
    {
        //凌晨两点和六点之间
        if(currentHour >= forcedSleepHour && currentHour < dayStartHour)
        {
            ForceSleep();
        }
    }

    /// <summary>
    /// 强制角色睡觉
    /// </summary>
    private void ForceSleep()
    {
        Debug.Log("强制角色睡觉");

        isSleeping = true;

        //加速时间比例尺
        timeScale *= nightTimeMultiplier;//晚上的时间加速

        //To do
        //睡觉时保存游戏
    }

    /// <summary>
    /// 更新游戏内的时间
    /// </summary>
    private void UpdateGameTime()
    {
        currentMinute += 10;    //每十分钟更新一次
        if (currentMinute >= 60)    //到达一小时
        {
            currentMinute = 0;
            currentHour += 1;

            if (currentHour >= 24)  //到达一天
            {
                currentHour = 0;
                currentDay += 1;
                weekday += 1;

                if (currentDay > dayPerSeason) //到达一季度
                {
                    currentDay = 1; //day从1开始

                    currentSeason = (Season)(((int)currentSeason + 1) % 4);//更新季节
                }
            }
        }

        //更新年份
        if(currentSeason == Season.Spring && currentDay == 1)
        {
            currentYear += 1;
        }

        UpdateTimeUI();
    }

    /// <summary>
    /// 更新游戏内时间的UI
    /// </summary>
    private void UpdateTimeUI()
    {
        _timeText.text = currentHour.ToString("D2") + ":" + currentMinute.ToString("D2");
        if (weekday > 7)
        {
            weekday -= 7;
        }
        _dateText.text = currentDay.ToString() + "日 " + "星期" + weekday;
    }
}
