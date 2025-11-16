using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生理系统
/// </summary>
public class PhysiologicalSystem : MonoBehaviour
{
    public static PhysiologicalSystem Instance;

    public EnergyModule energyModule;

    private void Awake()
    {
        Instance = this;

        energyModule = transform.Find("EnergyModule").GetComponent<EnergyModule>();
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
    /// 加载 玩家的生理系统数据
    /// </summary>
    /// <param name="saveData"></param>
    public void LoadGame(SaveData saveData)
    {
        energyModule.SetEnergy(saveData.physiologicalSaveData.energy);
    }

    /// <summary>
    /// 保存 玩家的生理系统数据
    /// </summary>
    /// <returns></returns>
    public PhysiologicalSaveData SaveGame()
    {
        PhysiologicalSaveData physiologicalSaveData = new PhysiologicalSaveData();
        physiologicalSaveData.energy = energyModule.currentEnergy;

        return physiologicalSaveData;
    }
}
