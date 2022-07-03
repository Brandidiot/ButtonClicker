using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building, IDataPersistence
{
    public new void LoadData(GameData data)
    {
        buildingLevel = data.mineLevel;
        buildingBaseCost = data.mineBaseCost;
        buildingCurrentCost = data.mineCurrentCost;
        upgradeAmount = data.mineUpgradeAmount;
        
        costText.text = "$" + NumberFormat.Instance.ShortNotation(buildingCurrentCost);
    }

    public new void SaveData(GameData data)
    {
        data.mineLevel = buildingLevel;
        data.mineBaseCost = buildingBaseCost;
        data.mineCurrentCost = buildingCurrentCost;
        data.mineUpgradeAmount = upgradeAmount;
    }
}
