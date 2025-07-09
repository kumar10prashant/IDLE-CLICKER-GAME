using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] GameObject ItemDataContainerprefab;
    public List<Upgrade_SO> upgradeList = new();
    public static ShopMenu Instance;

    [Header("Upgrade Events")]
    public static Action<double> autoClickerUpgradeEvent;
    public static Action<double> TapMultiplierUpgradeEvent;
    public static Action OfflineMultiplierUpgradeEvent;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < upgradeList.Count; i++)
        {



            GameObject g = Instantiate(ItemDataContainerprefab, parent);
            g.GetComponent<ItemDataContainer>().init(upgradeList[i].UpgradeName, upgradeList[i].UpgradeDescription, upgradeList[i].UpgradeIcon, upgradeList[i].UpgradePrice);
            int temp = i;
            g.GetComponent<ItemDataContainer>().button.onClick.AddListener(() => BuyUpgrade(upgradeList[temp], g.GetComponent<ItemDataContainer>().buttonText));
        }

    }

    public void BuyUpgrade(Upgrade_SO upgrade, TMP_Text text = null)
    {
        if (PlayerManager.Instance.current_Currency < (upgrade.UpgradePrice))
        {
            Debug.Log("Not enough currency to buy this upgrade.");
            return;
        }

        switch (upgrade.upgradeType)
        {
            case UpgradeType.AutoClicker:
                autoClickerUpgradeEvent?.Invoke(upgrade.UpgradePrice);

                break;
            case UpgradeType.TapMultiplier:
                TapMultiplierUpgradeEvent?.Invoke(upgrade.UpgradePrice);

                break;
            case UpgradeType.OfflineMultiplier:
                OfflineMultiplierUpgradeEvent?.Invoke();


                break;

        }
        upgrade.upgradeLevel++;
        PlayerManager.Instance.current_Currency -= (upgrade.UpgradePrice);
        upgrade.UpgradePrice = Mathf.Clamp((MathF.Pow((float)upgrade.upgradeMultiplier, (float)upgrade.upgradeLevel)), 0f, float.MaxValue);
        text.text = "Amount:\n" + MoneyFormatter.FormatMoney(upgrade.UpgradePrice);
    }


}
