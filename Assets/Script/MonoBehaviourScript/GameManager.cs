using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
   
    public double AutoMultiplier;
    public double TapMultiplier;
    public double offlineMultiplier;

    private void Awake()
    {
        Application.targetFrameRate = 61; // Set target frame rate to 60 FPS
        if (Instance == null)
        {
            Instance = this;
        }

    }
    private void Start()
    {
   
        
    }
    private void OnEnable()
    {
        ShopMenu.autoClickerUpgradeEvent += UpdateAutoMultiplier;
        ShopMenu.TapMultiplierUpgradeEvent += UpdateTapMultiplier;
        ShopMenu.OfflineMultiplierUpgradeEvent += UpdateOfflineMultiplier;
    }
    private void OnDisable()
    {
        ShopMenu.autoClickerUpgradeEvent -= UpdateAutoMultiplier;
        ShopMenu.TapMultiplierUpgradeEvent -= UpdateTapMultiplier;
        ShopMenu.OfflineMultiplierUpgradeEvent -= UpdateOfflineMultiplier;

    }

    public void UpdateAutoMultiplier(double value)
    {
        AutoMultiplier += value;
        if (AutoMultiplier < 0) AutoMultiplier = 0; // Prevent negative multipliers
    }

    public void UpdateTapMultiplier(double value)
    {
        TapMultiplier += value;
        if (TapMultiplier < 0) TapMultiplier = 0; // Prevent negative multipliers
    }

    public void UpdateOfflineMultiplier()
    {
       
        offlineMultiplier += 2;
    }
}
