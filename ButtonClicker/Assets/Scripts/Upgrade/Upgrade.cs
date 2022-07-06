using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDataPersistence
{
    public Building building;

    public UpgradeItem upgradeItem;

    [SerializeField] private string id;

    public bool purchased = false;

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
    }

    private void TaskOnClick()
    {
        //Upgrade has been purchased
        purchased = true;
        
        GameManager.Instance.Purchase(upgradeItem.BaseCost); //Remove currency
        GameManager.Instance.IncreaseMultiplier((int)upgradeItem.Multiplier); //Increase the GameManager multiplier
        building.UpgradeBuilding(upgradeItem.Multiplier); //Increase the Buildings multiplier
        
        //Hide Tooltip & Upgrade
        TooltipSystem.Hide();
        gameObject.SetActive(false);
    }

    #region Tooltip System

    //Tooltip Info
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(upgradeItem.Description, upgradeItem.Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
    
    #endregion

    #region Save System

    public void LoadData(GameData data)
    {
        data.upgradesPurchased.TryGetValue(id, out purchased);
        if (purchased)
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
        data.upgradesPurchased.Add(id, purchased);
    }
    
    #endregion
}
