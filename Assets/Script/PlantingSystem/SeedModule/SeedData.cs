using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class SeedData
{
    public int id;
    public int item_id;
    public string name;
    public string prefab_name;
    public int state_count;
    public List<int> timePerStage;
    public SeedType seed_type;

    public SeedData()
    {

    }

    public SeedData(SeedData seedData)
    {
        id = seedData.id;
        item_id = seedData.item_id;
        name = seedData.name;
        prefab_name = seedData.prefab_name;
        state_count = seedData.state_count;
        timePerStage = seedData.timePerStage.ToList();
        seed_type = seedData.seed_type;
    }
}
