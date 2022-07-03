using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : Building, IDataPersistence
{
    public new void LoadData(GameData data)
    {
        buildingLevel = data.grandmaLevel;
        buildingBaseCost = data.grandmaBaseCost;
        buildingCurrentCost = data.grandmaCurrentCost;
        upgradeAmount = data.grandmaUpgradeAmount;
        
        costText.text = "$" + NumberFormat.Instance.ShortNotation(buildingCurrentCost);
    }

    public new void SaveData(GameData data)
    {
        data.grandmaLevel = buildingLevel;
        data.grandmaBaseCost = buildingBaseCost;
        data.grandmaCurrentCost = buildingCurrentCost;
        data.grandmaUpgradeAmount = upgradeAmount;
    }
}
