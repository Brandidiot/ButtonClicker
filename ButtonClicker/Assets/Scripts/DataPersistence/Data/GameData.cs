[System.Serializable]
public class GameData
{
    //Game Data
    public float currentScore;
    public float scorePerSecondMultiplier;
    public float multiplier;
    public float scorePerSecond;
    
    //Auto Click Data
    public int clickLevel;
    public int clickBaseCost;
    public int clickCurrentCost;
    public float clickUpgradeAmount;
    
    //Grandma Data
    public int grandmaLevel;
    public int grandmaBaseCost;
    public int grandmaCurrentCost;
    public float grandmaUpgradeAmount;
    
    //Farm Data
    public int farmLevel;
    public int farmBaseCost;
    public int farmCurrentCost;
    public float farmUpgradeAmount;
    
    //Mine Data
    public int mineLevel;
    public int mineBaseCost;
    public int mineCurrentCost;
    public float mineUpgradeAmount;
    
    //Factory Data
    public int factoryLevel;
    public int factoryBaseCost;
    public int factoryCurrentCost;
    public float factoryUpgradeAmount;

    //Default Game Data
    public GameData()
    {
        currentScore = 0f;
        multiplier = 1f;
        scorePerSecond = 1f;
        scorePerSecondMultiplier = 0f;

        //Auto Click Data
        clickLevel = 1;
        clickBaseCost = 15;
        clickCurrentCost = clickBaseCost;
        clickUpgradeAmount = .1f;

        //Grandma Data
        grandmaLevel = 1;
        grandmaBaseCost = 100;
        grandmaCurrentCost = grandmaBaseCost;
        grandmaUpgradeAmount = 1;

        //Farm Data
        farmLevel = 1;
        farmBaseCost = 1100;
        farmCurrentCost = farmBaseCost;
        farmUpgradeAmount = 8;

        //Mine Data
        mineLevel = 1;
        mineBaseCost = 12000;
        mineCurrentCost = mineBaseCost;
        mineUpgradeAmount = 47;

        //Factory Data
        factoryLevel = 1;
        factoryBaseCost = 130000;
        factoryCurrentCost = factoryBaseCost;
        factoryUpgradeAmount = 260;
    }
}
