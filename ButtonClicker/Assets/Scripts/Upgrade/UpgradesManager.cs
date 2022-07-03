using UnityEngine;


/*
 * Instead of storing all upgrades under the upgrade game object. It might be more beneficial to store all
 * upgrades as prefabs and edit this manager to instantiate a new child object if the prefab is purchasable.
 */


public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private Upgrade[] upgrades;

    private void Start()
    {
        //Get # of upgrades and set upgrades array to amount of upgrades
        upgrades = new Upgrade[transform.childCount];
        
        //set upgrades
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<Upgrade>();
            upgrades[i] = child;
            upgrades[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        CheckUpgrades();
    }

    private void CheckUpgrades()
    {
        //Loop through all upgrades
        foreach (var upgrade in upgrades)
        {
            
            //If not enough money for the upgrade go to next upgrade
            if (!(GameManager.Instance.currentScore >= upgrade.upgradeItem.BaseCost)) continue;
            
            //if building level is lower than the required level than go to next upgrade
            if (upgrade.building.buildingLevel < upgrade.upgradeItem.requiredBuildingLevel + 1) continue;
            
            //If the upgrade isn't purchased than show the upgrade to purchase
            if (!upgrade.purchased)
            {
                upgrade.gameObject.SetActive(true);
            }
        }
    }
}
