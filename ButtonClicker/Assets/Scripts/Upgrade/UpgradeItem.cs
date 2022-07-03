using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Item", menuName = "Button Clicker/Upgrade Item")]
public class UpgradeItem : ScriptableObject
{
    public Sprite Sprite;
    public string Name = "Name";
    public string Description;
    public double BaseCost = 100;
    public int requiredBuildingLevel = 2;
    public float Multiplier = 2;
}