using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    /// <summary>
    /// 商店是否打开
    /// </summary>
    public bool isOpen = false;
    /// <summary>
    /// 关闭按钮
    /// </summary>
    public Button _closeButton;
    /// <summary>
    /// 控制商店面板的开关
    /// </summary>
    public CanvasGroup _canvasGroup;
    /// <summary>
    /// 商店物品prefab
    /// </summary>
    public GameObject shopItemPrefab;
    /// <summary>
    /// shop的滚动面板区域，用于展示商品
    /// </summary>
    public Transform _shopContentTrans;

    private void Awake()
    {
        //获取引用
        _canvasGroup        = GetComponent<CanvasGroup>();
        _closeButton        = transform.Find("ColseButton").GetComponent<Button>();
        shopItemPrefab      = Resources.Load<GameObject>("Prefab/ShopItem");
        _shopContentTrans   = transform.Find("Scroll View/Viewport/ShopContent");
        //shopContent       = GameObject.Find("ShopContent").transform;

        //添加点击事件
        _closeButton.onClick.AddListener(() =>
        {
            CloseShop();
        });

        //初始化
        CloseShop();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(ItemData itemData in DataManager.Instance.itemDataList)
        {
            GameObject go = Instantiate(shopItemPrefab, _shopContentTrans);

            //获取图标，名称，价格
            go.transform.Find("Icon").GetComponent<Image>().sprite = itemData.icon;
            go.transform.Find("Name").GetComponent<TMP_Text>().text = itemData.name;
            go.transform.Find("Money").GetComponent<TMP_Text>().text = itemData.price.ToString();

            //添加点击事件
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                //To do 先判断金币是否够用
                //if (金币足够)
                //{
                //    //消耗金币

                //    //添加物品
                      BackpackSystem.Instance.TryAddItem(itemData, 1);
                //}
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        //键盘输入控制商店开关
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isOpen)
            {
                CloseShop();
            }
            else
            {
                OpenShop();
            }
        }
    }

    /// <summary>
    /// 打开商店
    /// </summary>
    public void OpenShop()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        isOpen = true;
    }

    /// <summary>
    /// 关闭商店
    /// </summary>
    public void CloseShop()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        isOpen = false;
    }
}
