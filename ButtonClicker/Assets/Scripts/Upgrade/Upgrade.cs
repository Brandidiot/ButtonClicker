using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Upgrade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDataPersistence
{
    public Building building;

    public UpgradeItem upgradeItem;

    [SerializeField] private string id;

    public bool _purchased = false;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = Guid.NewGuid().ToString();
    }

    private Button _upgradeButton;

    private void Start()
    {
        //Listen To Button Event
        _upgradeButton = GetComponent<Button>();
        _upgradeButton.onClick.AddListener(TaskOnClick);

        //Set Sprite At Start
        GetComponent<Image>().sprite = upgradeItem.Sprite;
        
        /*Hide Image & Text
        GetComponent<Image>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }*/
    }

    private void TaskOnClick()
    {
        _purchased = true;
        GameManager.Instance.Purchase(upgradeItem.BaseCost);
        GameManager.Instance.IncreaseMultiplier((int)upgradeItem.Multiplier);
        building.UpgradeBuilding(upgradeItem.Multiplier);
        
        //Hide Tooltip
        TooltipSystem.Hide();
        gameObject.SetActive(false);
    }

    /*public void Update()
    {
        if (building.buildingLevel < upgradeItem.requiredBuildingLevel) return;
        
        if (GameManager.Instance.currentScore < upgradeItem.BaseCost)
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
    }*/

    //Tooltip Info
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(upgradeItem.Description, upgradeItem.Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

    public void LoadData(GameData data)
    {
        data.upgradesPurchased.TryGetValue(id, out _purchased);
        if (_purchased)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.upgradesPurchased.ContainsKey(id))
        {
            data.upgradesPurchased.Remove(id);
        }
        data.upgradesPurchased.Add(id, _purchased);
    }
}
