using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilPlot : MonoBehaviour
{
    /// <summary>
    /// 浇水后的土壤图片
    /// </summary>
    public GameObject _water;

    private void Awake()
    {
        _water = transform.Find("Water").gameObject;
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
    /// 给土地浇水
    /// </summary>
    public void Watered()
    {
        _water.SetActive(true);
    }

    /// <summary>
    /// 水干了
    /// </summary>
    public void Dry()
    {
        _water.SetActive(false);
    }

    /// <summary>
    /// 播种
    /// </summary>
    public void PlantSeed()
    {
        //To do
    }
}
