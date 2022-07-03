using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building, IDataPersistence
{
    public new void LoadData(GameData data)
    {
        buildingLevel = data.factoryLevel;
        buildingBaseCost = data.factoryBaseCost;
        buildingCurrentCost = data.factoryCurrentCost;
        upgradeAmount = data.factoryUpgradeAmount;
        
        costText.text = "$" + buildingCurrentCost;
    }

    public new void SaveData(GameData data)
    {
        data.factoryLevel = buildingLevel;
        data.factoryBaseCost = buildingBaseCost;
        data.factoryCurrentCost = buildingCurrentCost;
        data.factoryUpgradeAmount = upgradeAmount;
    }
}
