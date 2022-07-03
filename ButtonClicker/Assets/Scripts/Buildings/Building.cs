using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDataPersistence
{
    public TextMeshProUGUI costText;
    public int buildingLevel;
    public float buildingBaseCost = 15;
    public float buildingCurrentCost;
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
        costText.text = "$" + NumberFormat.Instance.ShortNotation(buildingCurrentCost);
        TooltipSystem.Hide();
        TooltipSystem.Show("each " + gameObject.name + " produces " + upgradeAmount + " per second." + "\n" +
                           (buildingLevel - 1) + " Auto Clicks producing " + (buildingLevel - 1) * upgradeAmount + " per second.");
    }

    public void UpgradeBuilding(float amount)
    {
        var originalUpgrade = upgradeAmount;
        upgradeAmount *= amount;
        GameManager.Instance.IncreaseSpsMultiplier((buildingLevel - 1) * upgradeAmount - (buildingLevel - 1) * originalUpgrade);
    }

    public void LoadData(GameData data)
    {
        costText.text = "$" + NumberFormat.Instance.ShortNotation(buildingCurrentCost);
    }

    public void SaveData(GameData data)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show("each " + gameObject.name + " produces " + upgradeAmount + " per second." + "\n" +
                           (buildingLevel - 1) + " Auto Clicks producing " + (buildingLevel - 1) * upgradeAmount + " per second.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}