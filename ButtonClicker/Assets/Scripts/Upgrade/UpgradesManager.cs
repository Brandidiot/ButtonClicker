using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private Upgrade[] upgrades;

    private void Start()
    {
        upgrades = new Upgrade[transform.childCount];
        
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<Upgrade>();
            upgrades[i] = child;
            upgrades[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        foreach (var upgrade in upgrades)
        {
            if (upgrade._purchased) return;

            if (GameManager.Instance.currentScore >= upgrade.upgradeItem.BaseCost 
                && upgrade.building.buildingLevel >= upgrade.upgradeItem.requiredBuildingLevel)
            {
                upgrade.gameObject.SetActive(true);
            }
        }
    }
}
