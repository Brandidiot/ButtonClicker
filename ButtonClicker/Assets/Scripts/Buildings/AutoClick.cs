using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClick : Building, IDataPersistence
{
    public new void LoadData(GameData data)
    {
        buildingLevel = data.clickLevel;
        buildingBaseCost = data.clickBaseCost;
        buildingCurrentCost = data.clickCurrentCost;
        upgradeAmount = data.clickUpgradeAmount;
        
        costText.text = "$" + buildingCurrentCost;
    }

    public new void SaveData(GameData data)
    {
        data.clickLevel = buildingLevel;
        data.clickBaseCost = buildingBaseCost;
        data.clickCurrentCost = buildingCurrentCost;
        data.clickUpgradeAmount = upgradeAmount;
    }
}
