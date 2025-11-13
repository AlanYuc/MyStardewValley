using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    public CanvasGroup _canvasGroup;
    /// <summary>
    /// 面板是否打开
    /// </summary>
    public bool isOpen;

    public virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        isOpen = false;
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
    /// 打开面板
    /// </summary>
    public virtual void OpenPanel()
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        isOpen = true;
    }

    /// <summary>
    /// 关闭面板
    /// </summary>
    public virtual void ClosePanel()
    {
        _canvasGroup.alpha = 0.0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        isOpen = false;
    }
}
