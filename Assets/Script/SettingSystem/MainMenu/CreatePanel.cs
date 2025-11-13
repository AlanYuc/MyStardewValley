using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreatePanel : BasePanel
{
    /// <summary>
    /// 确认按钮
    /// </summary>
    public Button _okButton;
    /// <summary>
    /// 返回按钮
    /// </summary>
    public Button _backButton;
    /// <summary>
    /// 在此区域内输入玩家名称
    /// </summary>
    public TMP_InputField _playerNameInputField;
    /// <summary>
    /// 在此区域内输入农场名称
    /// </summary>
    public TMP_InputField _farmNameInputField;
    /// <summary>
    /// 在此区域内输入玩家最喜欢的东西的名称
    /// </summary>
    public TMP_InputField _favoriteNameInputField;
    /// <summary>
    /// 玩家名称
    /// </summary>
    public string playerName;
    /// <summary>
    /// 农场名称
    /// </summary>
    public string farmName;
    /// <summary>
    /// 玩家最喜欢的东西的名称
    /// </summary>
    public string favoriteName;


    public override void Awake()
    {
        base.Awake();

        _okButton               = transform.Find("OK_Btn").GetComponent<Button>();
        _backButton             = transform.Find("Back_Btn").GetComponent<Button>();
        _playerNameInputField   = transform.Find("CreateRole_Bg/PlayerName_InputField").GetComponent<TMP_InputField>();
        _farmNameInputField     = transform.Find("CreateRole_Bg/FarmName_InputField").GetComponent<TMP_InputField>();
        _favoriteNameInputField = transform.Find("CreateRole_Bg/FavoriteName_InputField").GetComponent<TMP_InputField>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //添加事件监听
        _okButton.onClick.AddListener(OnOkButtonClick);
        _backButton.onClick.AddListener(OnBackButtonClick);
        _playerNameInputField.onEndEdit.AddListener(OnPlayerNameInput);
        _farmNameInputField.onEndEdit.AddListener(OnFarmNameInput);
        _favoriteNameInputField.onEndEdit.AddListener(OnFavoriteNameInput);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ClosePanel()
    {
        base.ClosePanel();

        ClearCache();
    }

    /// <summary>
    /// 清空缓存
    /// </summary>
    private void ClearCache()
    {
        //清空所有字符串
        _playerNameInputField.text = string.Empty;
        _farmNameInputField.text= string.Empty;
        _favoriteNameInputField.text = string.Empty;
        playerName = string.Empty;
        farmName = string.Empty;
        favoriteName = string.Empty;
    }

    public void OnOkButtonClick()
    {
        //保存数据，创建新存档，跳转到游戏场景
        SceneManager.LoadScene("Main");
    }

    public void OnBackButtonClick()
    {
        SettingSystem.Instance.OpenPanel(PanelType.MainMenu);
    }

    public void OnPlayerNameInput(string name)
    {
        playerName = name;
    }

    public void OnFarmNameInput(string name)
    {
        farmName = name;
    }

    public void OnFavoriteNameInput(string name)
    {
        favoriteName = name;
    }
}
