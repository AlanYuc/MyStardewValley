using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weed : MonoBehaviour
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
    /// ¸î²Ý
    /// </summary>
    public void Mow()
    {
        ItemData weedItemData = DataManager.Instance.GetItemData(101);

        BackpackSystem.Instance.TryAddItem(weedItemData, 1);

        Destroy(gameObject);
    }
}
