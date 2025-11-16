using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : BasePanel
{
    /// <summary>
    /// 返回按钮
    /// </summary>
    public Button _backButton;
    /// <summary>
    /// 存放所有存档的区域
    /// </summary>
    public Transform _loadContent;
    /// <summary>
    /// 存档列表显示的prefab
    /// </summary>
    public GameObject loadItem;

    public override void Awake()
    {
        base.Awake();

        _backButton     = transform.Find("Back_Btn").GetComponent<Button>();
        _loadContent    = transform.Find("LoadBackground_Bg/Scroll View/Viewport/Load_Content").GetComponent<Transform>();
        loadItem        = Resources.Load<GameObject>("Prefab/LoadItem");
    }

    // Start is called before the first frame update
    void Start()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 返回按钮点击事件
    /// </summary>
    private void OnBackButtonClick()
    {
        SettingSystem.Instance.OpenPanel(PanelType.MainMenu);
    }

    /// <summary>
    /// 打开面板，并且更新所有面板信息
    /// </summary>
    public override void OpenPanel()
    {
        base.OpenPanel();

        //读取所有存档
        List<SaveData> saveDataList = SaveSystem.Instance.GetSaveDataList();

        int i = 0;//索引

        //展示所有存档
        foreach (SaveData saveData in saveDataList)
        {
            //生成一个存档对象
            GameObject go = Instantiate(loadItem, _loadContent);

            //获取引用
            TMP_Text playerName = go.transform.Find("PlayerName").GetComponent<TMP_Text>();
            TMP_Text farmName   = go.transform.Find("FarmName").GetComponent<TMP_Text>();
            TMP_Text gameTime   = go.transform.Find("GameTime").GetComponent<TMP_Text>();
            TMP_Text realTime   = go.transform.Find("RealTime").GetComponent<TMP_Text>();
            TMP_Text money      = go.transform.Find("Money").GetComponent<TMP_Text>();
            TMP_Text number     = go.transform.Find("Number").GetComponent<TMP_Text>();
            Button button       = go.GetComponent<Button>();
            Button deleteButton = go.transform.Find("DeleteArchive").GetComponent<Button>();

            //游戏内时间
            gameTime.text = PrintGameTime(saveData.timeSaveData);
            //真实时间
            realTime.text = PrintRealTime(saveData.timeSaveData);

            //更新信息
            playerName.text = saveData.playerSaveData.player_name;
            farmName.text = saveData.playerSaveData.farm_name;
            money.text = saveData.tradeSaveData.coins.ToString();
            number.text = i.ToString() + ".";

            i++;

            //添加点击事件
            //点击对应存档加载游戏
            button.onClick.AddListener(() =>
            {
                SaveSystem.Instance.LoadArchive(saveData);
            });
            deleteButton.onClick.AddListener(() =>
            {
                //删除Resources目录下的存档
                string filePath = Path.Combine(
                    Application.dataPath,
                    $"Resources/Save/{saveData.playerSaveData.player_name}.json"
                    );

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);//删除文件
                    Debug.Log(saveData.playerSaveData.player_name + "存档已删除");
                }

                //删除存档对象
                Destroy(go);
            });
        }

        //修改scroll view的content区域的高度
        //SetContentHeight(saveDataList.Count);
    }

    

    /// <summary>
    /// 打印现实游玩时间
    /// </summary>
    /// <param name="realTimeSaveData"></param>
    /// <returns></returns>
    private string PrintRealTime(TimeSaveData realTimeSaveData)
    {
        //获取现实游玩的总小时数
        int hours = Mathf.FloorToInt(realTimeSaveData.realTimeSecond / 60.0f / 60.0f);

        //获取剩下的分钟数
        int minutes = Mathf.FloorToInt((realTimeSaveData.realTimeSecond % 3600) / 60);
        //int minutes = Mathf.FloorToInt(gameTimeSaveData.realTimeSecond / 60.0f) - hours * 60;

        return hours + " : " + minutes.ToString("D2");
    }

    /// <summary>
    /// 打印游戏内时间
    /// </summary>
    /// <param name="gameTimeSaveData"></param>
    /// <returns></returns>
    private string PrintGameTime(TimeSaveData gameTimeSaveData)
    {
        //年份 季节 日期

        string season;
        switch (gameTimeSaveData.currentSeason)
        {
            case Season.Spring:
                season = "春季";
                break;
            case Season.Summer:
                season = "夏季";
                break;
            case Season.Autumn:
                season = "秋季";
                break;
            case Season.Winter:
                season = "冬季";
                break;
            default:
                season = "";
                break;
        }

        string date =
            gameTimeSaveData.currentYear + "年" +
            season +
            gameTimeSaveData.currentDay + "日";

        return date;
    }

    public override void ClosePanel()
    {
        base.ClosePanel();

        //删除缓存
        //删除存档预制体

        int amount = _loadContent.childCount;//获取存档数量
        for(int i = amount - 1; i >= 0; i--)//注意从后往前删除
        {
            Destroy(_loadContent.GetChild(i).gameObject);
        }
    }

    public static void DeleteSave(SaveData saveData)
    {
        //删除Resources目录下的存档
        string filePath = Path.Combine(
            Application.dataPath,
            $"Resources/Save/{saveData.playerSaveData.player_name}.json"
            );

        if (File.Exists(filePath))
        {
            File.Delete(filePath);//删除文件
            Debug.Log(saveData.playerSaveData.player_name + "存档已删除");
        }
    }

    /// <summary>
    /// 修改scroll view的content区域的高度
    /// </summary>
    /// <param name="count"></param>
    private void SetContentHeight(int count)
    {
        RectTransform rectTransform = _loadContent.GetComponent<RectTransform>();
        Vector2 sizeDelta = rectTransform.sizeDelta;

        //一个存档对象的高度
        float saveItemHeight = loadItem.GetComponent<RectTransform>().sizeDelta.y;

        sizeDelta.y = Mathf.Max(saveItemHeight * count, 750);

        rectTransform.sizeDelta = sizeDelta;
    }
}
