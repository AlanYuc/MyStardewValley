using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    /// <summary>
    /// UI专用相机
    /// </summary>
    public Camera uiCamera;
    /// <summary>
    /// 画布
    /// </summary>
    public Canvas _canvas;
    /// <summary>
    /// 画布的RectTransform
    /// </summary>
    public RectTransform _canvasRect;

    private void Awake()
    {
        Instance = this;

        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        uiCamera = _canvas.worldCamera;
        _canvasRect = _canvas.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
