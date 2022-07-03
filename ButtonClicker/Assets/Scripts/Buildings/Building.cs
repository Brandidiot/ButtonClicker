using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI costText;
    public int buildingLevel;
    public int buildingBaseCost = 15;
    public int buildingCurrentCost;
    public float upgradeAmount = .1f;

    private void Update()
    {
        GetComponent<Button>().interactable = !(GameManager.Instance.currentScore < buildingCurrentCost);
    }
    
    public void BuildingPurchase()
    {
        if (!(GameManager.Instance.currentScore >= buildingCurrentCost)) return;

        GameManager.Instance.Purchase(buildingCurrentCost);
        GameManager.Instance.IncreaseSpsMultiplier(upgradeAmount);
        buildingCurrentCost = (int)(buildingCurrentCost * 1.15f);
        buildingLevel += 1;
        costText.text = "$" + buildingCurrentCost;
    }

    public void UpgradeBuilding(float amount)
    {
        var originalUpgrade = upgradeAmount;
        upgradeAmount *= amount;
        GameManager.Instance.IncreaseSpsMultiplier((buildingLevel - 1) * upgradeAmount - (buildingLevel - 1) * originalUpgrade);
    }

    public void LoadData(GameData data)
    {
        costText.text = "$" + buildingCurrentCost;
    }

    public void SaveData(GameData data)
    {
    }
}