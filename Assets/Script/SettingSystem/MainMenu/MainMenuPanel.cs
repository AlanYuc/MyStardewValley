using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主菜单面板
/// </summary>
public class MainMenuPanel : BasePanel
{
    /// <summary>
    /// 开始游戏按钮
    /// </summary>
    public Button _createGameButton;
    /// <summary>
    /// 加载游戏按钮
    /// </summary>
    public Button _loadGameButton;
    /// <summary>
    /// 捕鱼篓按钮
    /// </summary>
    public Button _fishBasketButton;
    /// <summary>
    /// 退出游戏按钮
    /// </summary>
    public Button _exitGameButton;

    public override void Awake()
    {
        base.Awake();

        _createGameButton = transform.Find("CreateGame_Btn").GetComponent<Button>();
        _loadGameButton = transform.Find("LoadGame_Btn").GetComponent<Button>();
        _fishBasketButton = transform.Find("FishBasket_Btn").GetComponent<Button>();
        _exitGameButton = transform.Find("ExitGame_Btn").GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _createGameButton.onClick.AddListener(() =>
        {
            SettingSystem.Instance.OpenPanel(PanelType.Create);
        });
        _loadGameButton.onClick.AddListener(() =>
        {
            SettingSystem.Instance.OpenPanel(PanelType.Load);
        });
        _fishBasketButton.onClick.AddListener(() =>
        {
            Debug.Log("捕鱼篓");
        });
        _exitGameButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
