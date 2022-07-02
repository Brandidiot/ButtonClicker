using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Building building;

    [SerializeField] private UpgradeItem upgradeItem;

    private Button _upgradeButton;

    private void Start()
    {
        //Listen To Button Event
        _upgradeButton = GetComponent<Button>();
        _upgradeButton.onClick.AddListener(TaskOnClick);

        //Set Sprite At Start
        GetComponent<Image>().sprite = upgradeItem.Sprite;
        
        //Hide Image & Text
        GetComponent<Image>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void TaskOnClick()
    {
        GameManager.Instance.Purchase(upgradeItem.BaseCost);
        GameManager.Instance.IncreaseMultiplier((int)upgradeItem.Multiplier);
        building.UpgradeBuilding(upgradeItem.Multiplier);
        
        //Hide Tooltip
        TooltipSystem.Hide();

        gameObject.SetActive(false);
    }

    public void Update()
    {
        if (building.BuildingLevel < upgradeItem.requiredBuildingLevel) return;
        
        if (GameManager.Instance.CurrentScore < upgradeItem.BaseCost)
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

    //Tooltip Info
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(upgradeItem.Description, upgradeItem.Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
