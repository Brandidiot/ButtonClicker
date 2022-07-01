using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI autoClickCostText;
    [SerializeField] private int buildingLevel = 1;
    [SerializeField] private int buildingBaseCost = 15;
    [SerializeField] private int buildingCurrentCost;
    [SerializeField] private float upgradeAmount = .1f;

    private void Start()
    {
        buildingCurrentCost = buildingBaseCost;
        autoClickCostText.text = "$" + buildingCurrentCost;
    }

    private void Update()
    {
        GetComponent<Button>().interactable = !(GameManager.Instance.CurrentScore < buildingCurrentCost);
    }

    public void UpgradePurchase()
    {
        if (!(GameManager.Instance.CurrentScore >= buildingCurrentCost)) return;

        GameManager.Instance.PurchaseUpgrade(buildingCurrentCost);
        GameManager.Instance.IncreaseSpsMultiplier(upgradeAmount);
        buildingCurrentCost = (int)(buildingCurrentCost * 1.15f);
        buildingLevel += 1;
        autoClickCostText.text = "$" + buildingCurrentCost;
    }

    public void UpgradeBuilding(float amount)
    {
        var originalUpgrade = upgradeAmount;
        upgradeAmount *= amount;
        GameManager.Instance.IncreaseSpsMultiplier((buildingLevel - 1) * upgradeAmount - (buildingLevel - 1) * originalUpgrade);
    }
}