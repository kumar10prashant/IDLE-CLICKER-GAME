using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public double AutoMultiplier;
    public double TapMultiplier;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

    }
    private void Start()
    {
        AutoMultiplier = 1; // Initial value for auto clicker multiplier
        TapMultiplier = 1; // Initial value for tap multiplier
    }
    private void OnEnable()
    {
        ShopMenu.autoClickerUpgradeEvent += UpdateAutoMultiplier;
        ShopMenu.TapMultiplierUpgradeEvent += UpdateTapMultiplier;
    }
    private void OnDisable()
    {
        ShopMenu.autoClickerUpgradeEvent -= UpdateAutoMultiplier;
        ShopMenu.TapMultiplierUpgradeEvent -= UpdateTapMultiplier;
    }

    public void UpdateAutoMultiplier(double value)
    {
        Debug.Log(value);
        AutoMultiplier += value;
        if (AutoMultiplier < 0) AutoMultiplier = 0; // Prevent negative multipliers
    }

    public void UpdateTapMultiplier(double value)
    {
        TapMultiplier += value;
        if (TapMultiplier < 0) TapMultiplier = 0; // Prevent negative multipliers
    }
}
