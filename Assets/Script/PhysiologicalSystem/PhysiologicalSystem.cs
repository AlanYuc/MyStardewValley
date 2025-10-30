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
}
