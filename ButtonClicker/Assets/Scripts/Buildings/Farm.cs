using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building, IDataPersistence
{
    public new void LoadData(GameData data)
    {
        buildingLevel = data.farmLevel;
        buildingBaseCost = data.farmBaseCost;
        buildingCurrentCost = data.farmCurrentCost;
        upgradeAmount = data.farmUpgradeAmount;
        
        costText.text = "$" + buildingCurrentCost;
    }

    public new void SaveData(GameData data)
    {
        data.farmLevel = buildingLevel;
        data.farmBaseCost = buildingBaseCost;
        data.farmCurrentCost = buildingCurrentCost;
        data.farmUpgradeAmount = upgradeAmount;
    }
}
