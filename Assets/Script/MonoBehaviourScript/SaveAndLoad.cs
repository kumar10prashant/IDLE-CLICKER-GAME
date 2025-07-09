using System;
using System.Collections.Generic;


[System.Serializable]
public class SaveAndLoad
{
   public double autoMultiplier;
   public double tapMultiplier;
   public double offlineMultiplier;
    public double current_Currency;
    public long lastSaveTime; 
    public List<UpgradeData> upgradeData;

}
[System.Serializable]  
public class UpgradeData
{
    public int UniqueId;
    public double UpgradePrice;
    public double upgradeLevel;
    public bool isPurchased;
    public bool canBeHidden;
}
