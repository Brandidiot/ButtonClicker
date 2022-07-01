using System;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private Building building;
    [SerializeField] private int upgradeBaseCost = 100;
    [SerializeField] private int requiredBuildingLevel = 2;
    [SerializeField] private float upgradeMultiplier = 2;

    private Button _upgradeButton;

    private void Start()
    {
        _upgradeButton = GetComponent<Button>();
        _upgradeButton.onClick.AddListener(TaskOnClick);
        
        //Hide Image & Text
        GetComponent<Image>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void TaskOnClick()
    {
        GameManager.Instance.Purchase(upgradeBaseCost);
        GameManager.Instance.IncreaseMultiplier((int)upgradeMultiplier);
        building.UpgradeBuilding(upgradeMultiplier);

        gameObject.SetActive(false);
    }

    public void Update()
    {
        if (building.BuildingLevel < requiredBuildingLevel) return;
        
        if (GameManager.Instance.CurrentScore < upgradeBaseCost)
        {
            GetComponent<Image>().enabled = false;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            GetComponent<Image>().enabled = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
