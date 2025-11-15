using System;
using System.Collections;
using System.Collections.Generic;
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

    private void OnBackButtonClick()
    {
        SettingSystem.Instance.OpenPanel(PanelType.MainMenu);
    }

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
            TMP_Text farmName = go.transform.Find("FarmName").GetComponent<TMP_Text>();
            TMP_Text gameTime = go.transform.Find("GameTime").GetComponent<TMP_Text>();
            TMP_Text realTime = go.transform.Find("RealTime").GetComponent<TMP_Text>();
            TMP_Text money = go.transform.Find("Money").GetComponent<TMP_Text>();
            TMP_Text number = go.transform.Find("Number").GetComponent<TMP_Text>();

            //游戏内时间
            //真实时间

            //更新信息
            playerName.text = saveData.playerSaveData.player_name;
            farmName.text = saveData.playerSaveData.farm_name;
            money.text = saveData.tradeSaveData.coins.ToString();
            number.text = i.ToString();

            i++;
        }

        //点击对应存档加载游戏
    }

    public override void ClosePanel()
    {
        base.ClosePanel();

        //删除缓存
        //删除存档预制体
    }
}
