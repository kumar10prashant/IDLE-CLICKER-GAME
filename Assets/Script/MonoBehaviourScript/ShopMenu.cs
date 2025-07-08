using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] GameObject ItemDataContainerprefab;
    [SerializeField] List<Upgrade_SO> upgradeList = new();


    [Header("Upgrade Events")]
    [SerializeField] public static Action<double> autoClickerUpgradeEvent;
    [SerializeField] public static Action<double> TapMultiplierUpgradeEvent;
    [SerializeField] public static Action OfflineMultiplierUpgradeEvent;

    private void Start()
    {
        for (int i = 0; i < upgradeList.Count; i++)
        {
            upgradeList[i].UpgradePrice = 1;
            upgradeList[i].upgradeLevel = 1;
          

            GameObject g = Instantiate(ItemDataContainerprefab, parent);
            g.GetComponent<ItemDataContainer>().init(upgradeList[i].UpgradeName, upgradeList[i].UpgradeDescription, upgradeList[i].UpgradeIcon, upgradeList[i].UpgradePrice);
            int temp = i;
            g.GetComponent<ItemDataContainer>().button.onClick.AddListener(() => BuyUpgrade(upgradeList[temp],g.GetComponent<ItemDataContainer>().buttonText));
        }
    }

    public void BuyUpgrade(Upgrade_SO upgrade,TMP_Text text = null)
    {
        if(PlayerManager.Instance.current_Currency < (upgrade.UpgradePrice))
        {
            Debug.Log("Not enough currency to buy this upgrade.");
            return;
        }
        
        switch (upgrade.upgradeType)
        {
            case UpgradeType.AutoClicker:
                autoClickerUpgradeEvent?.Invoke(upgrade.UpgradePrice);
                upgrade.upgradeMultiplier = GameManager.Instance.AutoMultiplier;
                    break;
            case UpgradeType.TapMultiplier:
               TapMultiplierUpgradeEvent?.Invoke(upgrade.UpgradePrice);
                upgrade.upgradeMultiplier = GameManager.Instance.TapMultiplier;
                break;
            case UpgradeType.OfflineMultiplier:
                break;
                    
        }
        PlayerManager.Instance.current_Currency -= (upgrade.UpgradePrice);
        upgrade.UpgradePrice *= Mathf.Clamp((MathF.Pow((float)upgrade.upgradeMultiplier,(float)upgrade.upgradeLevel)),0f,float.MaxValue);
        text.text = "Amount:\n" + MoneyFormatter.FormatMoney(upgrade.UpgradePrice);
    }

   
}
