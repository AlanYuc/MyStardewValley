using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 存档系统
/// </summary>
public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
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
    /// 新建存档
    /// </summary>
    public void CreateNewArchive()
    {
        Debug.Log("新建存档");

        //读取内置存档 只读
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/DefaultSave");
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(jsonFile.text);

        //获取输入数据，同步到存档
        saveData.playerSaveData.player_name = SettingSystem.Instance.createPanel.playerName;
        saveData.playerSaveData.farm_name = SettingSystem.Instance.createPanel.farmName;
        saveData.playerSaveData.favorite = SettingSystem.Instance.createPanel.favoriteName;

        //将存档写入本地
        SaveWriteFile(saveData);

        //按存档初始化游戏
        SwitchSceneAndLoadGame(saveData);
    }

    /// <summary>
    /// 加载存档
    /// </summary>
    /// <param name="saveData"></param>
    public void LoadArchive(SaveData saveData)
    {
        SwitchSceneAndLoadGame(saveData);
    }

    /// <summary>
    /// 切换到游戏场景，并且加载游戏
    /// </summary>
    /// <param name="saveData"></param>
    private void SwitchSceneAndLoadGame(SaveData saveData)
    {
        //先切换scene
        SceneManager.LoadScene("Main");

        //再加载数据
        StartCoroutine(LoadGame(saveData));
    }

    /// <summary>
    /// 加载游戏
    /// </summary>
    /// <param name="saveData"></param>
    private IEnumerator LoadGame(SaveData saveData)
    {
        //等待0.5s
        yield return new WaitForSeconds(0.5f);

        //通知各个系统加载数据
        TimeSystem.Instance.LoadGame(saveData);//时间系统
        BackpackSystem.Instance.LoadGame(saveData);//背包系统
        TradeSystem.Instance.LoadGame(saveData);//交易系统
        PhysiologicalSystem.Instance.LoadGame(saveData);//生理系统
        PlantingSystem.Instance.LoadGame(saveData);//种植系统
        EnvironmentSystem.Instance.LoadGame(saveData);//环境系统
        Player.Instance.LoadGame(saveData);//玩家
    }

    /// <summary>
    /// 将saveData的数据保存到本地文件
    /// </summary>
    /// <param name="saveData"></param>
    private void SaveWriteFile(SaveData saveData)
    {
        //序列化 - 要写入的内容
        string content = JsonConvert.SerializeObject(saveData);
        //要写入的路径目录
        string filePath = Path.Combine(
            Application.dataPath,
            $"Resources/Save/{saveData.playerSaveData.player_name}.json"
            );
        //开始写入
        File.WriteAllText(filePath, content);

        Debug.Log(saveData.playerSaveData.player_name + "的存档写入数据完成");
    }

    /// <summary>
    /// 获取存档列表
    /// </summary>
    /// <returns></returns>
    public List<SaveData> GetSaveDataList()
    {
        Debug.Log("获取存档列表");

        List<SaveData> saveDataList = new List<SaveData>();

        TextAsset[] loadAll = Resources.LoadAll<TextAsset>("Save");

        foreach (TextAsset loadData in loadAll)
        {
            SaveData saveData = JsonConvert.DeserializeObject<SaveData>(loadData.text);
            saveDataList.Add(saveData);
        }

        return saveDataList;
    }

    /// <summary>
    /// 保存存档 - 将数据写入到文件当中
    /// 睡觉时自动保存，因此由时间系统调用
    /// </summary>
    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        //通知各个系统加载数据
        saveData.playerSaveData         = Player.Instance.SaveGame();//玩家
        saveData.timeSaveData           = TimeSystem.Instance.SaveGame();//时间系统
        saveData.backpackSaveData       = BackpackSystem.Instance.SaveGame();//背包系统
        saveData.tradeSaveData          = TradeSystem.Instance.SaveGame();//交易系统
        saveData.physiologicalSaveData  = PhysiologicalSystem.Instance.SaveGame();//生理系统
        saveData.plantSaveData          = PlantingSystem.Instance.SaveGame();//种植系统
        saveData.environmentSaveData    = EnvironmentSystem.Instance.SaveGame();//环境系统

        //写入本地
        SaveWriteFile(saveData);
    }
}