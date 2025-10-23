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
    /// <summary>
    /// 图集字典
    /// </summary>
    public Dictionary<string, Sprite[]> atlasDict = new Dictionary<string , Sprite[]>();

    private void Awake()
    {
        Instance = this;

        //获取item的Json数据
        itemDataList = LoadJsonList<ItemData>("Data/ItemData");

        //加载图集
        LoadAltas("Image/Items/springobjects", "item");

        //处理图集，把每个图片和它的名字保存到spriteDict的字典里
        AddToSpriteDict("item");

        //把图片icon注入到ItemData中
        foreach(ItemData itemData in itemDataList)
        {
            if (spriteDict.ContainsKey(itemData.iconName))
            {
                itemData.icon = spriteDict[itemData.iconName];
            }
        }
    }

    /// <summary>
    /// 将图集中的图片全部添加到字典中
    /// </summary>
    /// <param name="key">指定的图集</param>
    private void AddToSpriteDict(string key)
    {
        foreach (Sprite sprite in atlasDict[key])
        {
            spriteDict.Add(sprite.name, sprite);
        }
    }

    /// <summary>
    /// 加载图集
    /// </summary>
    /// <param name="path">图集的路径</param>
    /// <param name="key">基础item类型</param>
    private void LoadAltas(string path, string key)
    {
        Sprite[] atlas = Resources.LoadAll<Sprite>(path);
        atlasDict.Add(key, atlas);
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
