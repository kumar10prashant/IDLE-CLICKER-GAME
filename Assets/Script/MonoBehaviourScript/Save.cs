using Newtonsoft.Json;
using System;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
public class Save : MonoBehaviour {

    [SerializeField]List<Upgrade_SO> upgrades = new List<Upgrade_SO>();


   
    private void Start()
    {
        // Load the game when the script starts
        LoadGame();
    }
    private void OnApplicationFocus(bool focus)
    {
#if !UNITY_EDITOR
        if (!focus)
        {
           
            UpdateSave();
        }
#endif
    }
    public void OnApplicationQuit()
    {
        // Save the game when the application quits
        UpdateSave();
    }
    
    [ContextMenu("Save")]
private void UpdateSave()
    {
        SaveAndLoad save = new SaveAndLoad() {
#if !UNITY_EDITOR
            autoMultiplier = GameManager.Instance.AutoMultiplier,
            tapMultiplier = GameManager.Instance.TapMultiplier,
            current_Currency = PlayerManager.Instance.current_Currency,
            offlineMultiplier =0,
            lastSaveTime = DateTime.Now.Ticks,
         
#else
            autoMultiplier = 1.0, // Default value for testing in editor
            tapMultiplier = 1.0, // Default value for testing in editor
            current_Currency = 0, // Default value for testing in editor
            offlineMultiplier = 0, // Default value for testing in editor
            lastSaveTime = DateTime.Now.Ticks, // Current time in ticks
#endif
            upgradeData = upgrades.Select(u => new UpgradeData
            {
                UniqueId = u.UniqueId,
                UpgradePrice = u.UpgradePrice,
                upgradeLevel = u.upgradeLevel,
                isPurchased = u.isPurchased,
                canBeHidden = u.canBeHidden
            }).ToList()
            //upgrades =ShopMenu.Instance.upgradeList
        };


        string json = JsonConvert.SerializeObject(save);
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log(path);
        File.WriteAllText(path, json);
        Debug.Log(json);
        
    }

    public void LoadGame()
    {
        if(File.Exists(Path.Combine(Application.persistentDataPath, "save.json")))
        {
            string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, "save.json"));
            SaveAndLoad save = JsonConvert.DeserializeObject<SaveAndLoad>(json);
            Debug.Log(json);
            GameManager.Instance.AutoMultiplier = save.autoMultiplier;
            GameManager.Instance.TapMultiplier = save.tapMultiplier;
            GameManager.Instance.offlineMultiplier = save.offlineMultiplier;
            long ticksNow = DateTime.Now.Ticks;
            double secondsOffline = (ticksNow - save.lastSaveTime) / (double)TimeSpan.TicksPerSecond;

            // Calculate earnings
            double offlineEarnings = Math.Clamp(save.autoMultiplier * secondsOffline * save.offlineMultiplier,0,40000000);
            Debug.Log(offlineEarnings);
            PlayerManager.Instance.current_Currency = save.current_Currency + offlineEarnings;
            //ShopMenu.Instance.upgradeList = save.upgrades;
            foreach (var upgradeData in save.upgradeData)
            {
                Upgrade_SO upgrade = upgrades.FirstOrDefault(u => u.UniqueId == upgradeData.UniqueId);
                if (upgrade != null)
                {
                    upgrade.UpgradePrice = upgradeData.UpgradePrice;
                    upgrade.upgradeLevel = upgradeData.upgradeLevel;
                    upgrade.isPurchased = upgradeData.isPurchased;
                    upgrade.canBeHidden = upgradeData.canBeHidden;
                }
            }
            Debug.Log("Game Loaded Successfully");
        }
        else
        {
            Debug.LogWarning("Save file not found.");
            GameManager.Instance.AutoMultiplier = 1;
            GameManager.Instance.TapMultiplier = 1;
            GameManager.Instance.offlineMultiplier = 0;
            PlayerManager.Instance.current_Currency = 0;

        }
    }
    
    public void Reset()
    {
      
     

        UpdateSave();

    }
}
