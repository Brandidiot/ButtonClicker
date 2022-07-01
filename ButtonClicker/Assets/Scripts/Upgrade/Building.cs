using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI autoClickCostText;
    public int BuildingLevel { get; private set; }
    [SerializeField] private int buildingBaseCost = 15;
    [SerializeField] private int buildingCurrentCost;
    [SerializeField] private float upgradeAmount = .1f;

    private void Start()
    {
        BuildingLevel = 1;
        buildingCurrentCost = buildingBaseCost;
        autoClickCostText.text = "$" + buildingCurrentCost;
    }

    private void Update()
    {
        GetComponent<Button>().interactable = !(GameManager.Instance.CurrentScore < buildingCurrentCost);
    }

    public void BuildingPurchase()
    {
        if (!(GameManager.Instance.CurrentScore >= buildingCurrentCost)) return;

        GameManager.Instance.Purchase(buildingCurrentCost);
        GameManager.Instance.IncreaseSpsMultiplier(upgradeAmount);
        buildingCurrentCost = (int)(buildingCurrentCost * 1.15f);
        BuildingLevel += 1;
        autoClickCostText.text = "$" + buildingCurrentCost;
    }

    public void UpgradeBuilding(float amount)
    {
        var originalUpgrade = upgradeAmount;
        upgradeAmount *= amount;
        GameManager.Instance.IncreaseSpsMultiplier((BuildingLevel - 1) * upgradeAmount - (BuildingLevel - 1) * originalUpgrade);
    }
}