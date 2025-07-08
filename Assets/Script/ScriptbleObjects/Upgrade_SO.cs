using UnityEngine;


public enum UpgradeType
{
    AutoClicker,
    TapMultiplier,
    OfflineMultiplier,
  
}
[CreateAssetMenu(fileName = "New Upgrade", menuName = "CreateNewUpgrade/New Upgrade")]
public class Upgrade_SO : ScriptableObject
{
    public Sprite UpgradeIcon;
    public string UpgradeName;
    public string UpgradeDescription;
    public double UpgradePrice;
    public double upgradeLevel;
    public double upgradeMultiplier;
    public bool isPurchased,canBeHidden;
    public UpgradeType upgradeType;
   
}
