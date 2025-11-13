using System;
using System.Collections;
using System.Collections.Generic;
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

    public override void Awake()
    {
        base.Awake();

        _backButton     = transform.Find("Back_Btn").GetComponent<Button>();
        _loadContent    = transform.Find("LoadBackground_Bg/Scroll View/Viewport/Load_Content").GetComponent<Transform>();
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

        //读取所有存档，并展示
        //点击对应存档加载游戏
    }

    public override void ClosePanel()
    {
        base.ClosePanel();

        //删除缓存
        //删除存档预制体
    }
}
