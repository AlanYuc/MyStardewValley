using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ¿³Ê÷Ö¦
    /// </summary>
    public void Chop()
    {
        ItemData branchItemData = DataManager.Instance.GetItemData(102);

        BackpackSystem.Instance.TryAddItem(branchItemData, 1);

        Destroy(gameObject);
    }
}
