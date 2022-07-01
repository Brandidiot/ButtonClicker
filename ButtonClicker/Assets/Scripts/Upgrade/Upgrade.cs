using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI autoClickCostText;
    [SerializeField] private int upgradeLevel = 1;
    [SerializeField] private int upgradeBaseCost = 15;
    [SerializeField] private int upgradeCurrentCost;
    [SerializeField] private float upgradeAmount = .1f;

    private void Start()
    {
        upgradeCurrentCost = upgradeBaseCost;
        autoClickCostText.text = "$" + upgradeCurrentCost;
    }

    private void Update()
    {
        GetComponent<Button>().interactable = !(GameManager.Instance.CurrentScore < upgradeCurrentCost);
    }

    public void UpgradePurchase()
    {
        if (!(GameManager.Instance.CurrentScore >= upgradeCurrentCost)) return;

        GameManager.Instance.PurchaseUpgrade(upgradeCurrentCost);
        GameManager.Instance.IncreaseSpsMultiplier(upgradeAmount);
        upgradeCurrentCost = (int)(upgradeCurrentCost * 1.15f);
        upgradeLevel += 1;
        autoClickCostText.text = "$" + upgradeCurrentCost;
    }
}