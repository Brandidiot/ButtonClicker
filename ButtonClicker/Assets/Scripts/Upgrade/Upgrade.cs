using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private Building building;
    [SerializeField] private int upgradeBaseCost = 100;

    public void Update()
    {
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
