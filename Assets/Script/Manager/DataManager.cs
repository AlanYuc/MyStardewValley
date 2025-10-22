using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    /// <summary>
    /// 所有ItemData的数据
    /// </summary>
    public List<ItemData> itemDataList = new List<ItemData>();
    /// <summary>
    /// 物品图片icon的字典
    /// </summary>
    public Dictionary<string , Sprite> spriteDict = new Dictionary<string , Sprite>();

    private void Awake()
    {
        Instance = this;

        itemDataList = LoadJsonList<ItemData>("Data/ItemData");
    }

    /// <summary>
    /// 读取json列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private List<T> LoadJsonList<T>(string path)
    {
        //读取文本
        TextAsset jsonFile = Resources.Load<TextAsset>(path);

        //反序列化，获取文本数据
        List<T> list = JsonConvert.DeserializeObject<List<T>>(jsonFile.text);

        return list;
    }
}
