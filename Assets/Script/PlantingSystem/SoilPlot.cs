using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilPlot : MonoBehaviour
{
    /// <summary>
    /// 浇水后的土壤图片
    /// </summary>
    public GameObject _water;
    /// <summary>
    /// 该土壤地块上种植的植物
    /// </summary>
    public Plant plant;

    private void Awake()
    {
        _water = transform.Find("Water").gameObject;
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
    /// 给土地浇水
    /// </summary>
    public void Watered()
    {
        _water.SetActive(true);
    }

    /// <summary>
    /// 水干了
    /// </summary>
    public void Dry()
    {
        _water.SetActive(false);
    }

    /// <summary>
    /// 播种
    /// </summary>
    public void PlantSeed(ItemData itemData)
    {
        //根据ItemData信息找到对应的SeedData
        SeedData seedData = DataManager.Instance.seedDataList.Find(
            seed => seed.item_id == itemData.id
            );

        //拿到prefab
        GameObject seedPrefab = DataManager.Instance.prefabDict[seedData.prefab_name];

        //生成种子并拿到其引用
        plant = Instantiate(seedPrefab, transform.position, Quaternion.identity, transform).GetComponent<Plant>();

        //注入数据
        plant.SetData(seedData);

        //设置种子为第一阶段
        plant.SetGrowthStage(1);
    }
}
