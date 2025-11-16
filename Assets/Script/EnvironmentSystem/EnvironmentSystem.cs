using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 环境系统
/// </summary>
public class EnvironmentSystem : MonoBehaviour
{
    public static EnvironmentSystem Instance;

    private void Awake()
    {
        Instance = this;
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
    /// 加载游戏的环境物体数据
    /// </summary>
    /// <param name="saveData"></param>
    public void LoadGame(SaveData saveData)
    {
        foreach(KeyValuePair<int ,List<EasyData>> keyValuePair in saveData.environmentSaveData.stateless_objects)
        {
            //物品id
            int id = keyValuePair.Key;

            //生成物品
            if (!DataManager.Instance.prefabDict.ContainsKey(id.ToString()))
            {
                //遍历该id的每一个对象
                foreach(EasyData easyData in keyValuePair.Value)
                {
                    GameObject go = Instantiate(DataManager.Instance.prefabDict[id.ToString()]);
                    go.transform.position = new Vector3(easyData.x, easyData.y, easyData.z);
                }
            }
        }
    }

    /// <summary>
    /// 保存游戏的环境物体数据
    /// </summary>
    /// <returns></returns>
    public EnvironmentSaveData SaveGame()
    {
        EnvironmentSaveData environmentSaveData = new EnvironmentSaveData();

        //保存 石头 id 100 树木103 树枝102 草丛101
        environmentSaveData.stateless_objects.Add(100, SaveStatelessObject("Rock"));
        environmentSaveData.stateless_objects.Add(101, SaveStatelessObject("Weed"));
        environmentSaveData.stateless_objects.Add(102, SaveStatelessObject("Branch"));
        environmentSaveData.stateless_objects.Add(103, SaveStatelessObject("Tree"));
        
        return null;
    }

    /// <summary>
    /// 根据标签获取所有对象的list
    /// </summary>
    /// <param name="objectTag">对象的标签</param>
    /// <returns></returns>
    private List<EasyData> SaveStatelessObject(string objectTag)
    {
        List<EasyData> list = new List<EasyData>();

        //搜索所有具有objectTag标签的物体
        GameObject[] goList = GameObject.FindGameObjectsWithTag(objectTag);

        foreach (GameObject go in goList)
        {
            EasyData data = new EasyData();
            data.x = go.transform.position.x;
            data.y = go.transform.position.y;
            data.z = go.transform.position.z;
            list.Add(data);
        }

        return list;
    }
}
