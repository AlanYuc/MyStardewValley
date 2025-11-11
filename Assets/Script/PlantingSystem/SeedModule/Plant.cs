using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

/// <summary>
/// 种植的植物
/// </summary>
public class Plant : MonoBehaviour
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public SeedData seedData;
    /// <summary>
    /// 种子当前的生长阶段
    /// </summary>
    public int currentStage;
    /// <summary>
    /// 生长计时器
    /// </summary>
    public float growthTimer;
    /// <summary>
    /// 当前阶段的时间
    /// </summary>
    public float timePerStage;
    /// <summary>
    /// 所有阶段的图集数组
    /// </summary>
    public Sprite[] stageSprite;
    /// <summary>
    /// 渲染组件
    /// </summary>
    public SpriteRenderer _spriteRenderer;
    /// <summary>
    /// 是否可以生成
    /// 比如，未浇水的话就不能生长
    /// </summary>
    public bool isGrowing;
    /// <summary>
    /// 是否成熟
    /// </summary>
    public bool isMature;
    /// <summary>
    /// 是否在玩家附近
    /// </summary>
    public bool isNearPlayer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        currentStage = 1;
        growthTimer = 0;
        timePerStage = 0;
        isGrowing = false;
        isMature = false;
        isNearPlayer = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //判断是否有浇水，有的话自动开始生长
        bool isWatered = PlantingSystem.Instance.CheckWater(transform.position);
        if (isWatered)
        {
            StartGrow();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //不生长
        if (!isGrowing)
        {
            return;
        }

        //成熟了
        if (isMature)
        {
            //玩家在旁边的时候按E，进行收获
            if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
            {
                Harvest();
            }

            return;
        }

        //定时器累加 并切换阶段
        if(currentStage < seedData.state_count)
        {
            //当前种子未达到最终阶段，那么继续生长
            //需要跟时间系统保持一致，晚上的时候需要加速
            growthTimer += Time.deltaTime * TimeSystem.Instance.timeScale;

            //经过一个生长阶段，开始进入下一个阶段
            //growthTimer和timePerStage分别乘了timeScale和staticTimeScale
            //都将现实时间转成游戏内时间，并且在夜晚产生时间差，实现加速生长
            if (growthTimer >= timePerStage)
            {
                growthTimer = 0;

                SetGrowthStage(currentStage + 1);
            }
        }
        else
        {
            Mature();
        }
    }

    /// <summary>
    /// 作物成熟
    /// </summary>
    private void Mature()
    {
        isMature = true;

        PlantingSystem.Instance.WaterDry(transform.position);

        //UpdateSprite();
    }

    /// <summary>
    /// 设置种子生长的阶段
    /// </summary>
    /// <param name="nextStage"></param>
    public void SetGrowthStage(int nextStage)
    {
        currentStage = nextStage;

        //切换到下一阶段
        if (currentStage <= seedData.timePerStage.Count)
        {
            //获取当前阶段所需的生长时间
            timePerStage = seedData.timePerStage[currentStage - 1];//list下标减一
            //将时间转换为游戏内时间 需要静态的staticTimeScale
            timePerStage *= TimeSystem.Instance.staticTimeScale;
        }

        UpdateSprite();
    }

    /// <summary>
    /// 更新图片
    /// 除了每个阶段的图片，还有成熟的图片
    /// </summary>
    public void UpdateSprite()
    {
        //注意下标越界
        if(currentStage - 1 < stageSprite.Length)
        {
            _spriteRenderer.sprite = stageSprite[currentStage - 1];
        }
    }

    /// <summary>
    /// 收获成熟的农作物
    /// </summary>
    public void Harvest()
    {
        PlantingSystem.Instance.Harvest(transform.position);
        Destroy(gameObject);
    }

    /// <summary>
    /// 设置种子数据
    /// </summary>
    /// <param name="seedData"></param>
    public void SetData(SeedData seedData)
    {
        this.seedData = new SeedData(seedData);
    }

    /// <summary>
    /// 种子开始生长
    /// </summary>
    public void StartGrow()
    {
        isGrowing = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }
}
