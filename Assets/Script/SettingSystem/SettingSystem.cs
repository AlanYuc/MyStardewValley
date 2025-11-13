using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelType
{
    /// <summary>
    /// 主菜单
    /// </summary>
    MainMenu,
    /// <summary>
    /// 创建角色
    /// </summary>
    Create,
    /// <summary>
    /// 加载
    /// </summary>
    Load,
}

/// <summary>
/// 设置系统
/// </summary>
public class SettingSystem : MonoBehaviour
{
    public static SettingSystem Instance;

    public MainMenuPanel mainMenuPanel;
    public CreatePanel createPanel;
    public LoadPanel loadPanel;

    private void Awake()
    {
        Instance = this;

        mainMenuPanel   = GameObject.Find("MainMenuPanel").GetComponent<MainMenuPanel>();
        createPanel     = GameObject.Find("CreatePanel").GetComponent<CreatePanel>();
        loadPanel       = GameObject.Find("LoadPanel").GetComponent<LoadPanel>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OpenPanel(PanelType.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel(PanelType panelType)
    {
        mainMenuPanel.ClosePanel();
        createPanel.ClosePanel();
        loadPanel.ClosePanel();

        switch (panelType)
        {
            case PanelType.MainMenu:
                mainMenuPanel.OpenPanel();
                break;
            case PanelType.Create:
                createPanel.OpenPanel();
                break;
            case PanelType.Load:
                loadPanel.OpenPanel();
                break;
            default:
                break;
        }
    }
}
