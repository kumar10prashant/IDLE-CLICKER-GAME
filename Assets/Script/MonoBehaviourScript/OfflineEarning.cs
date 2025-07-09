using System;
using UnityEngine;

public class OfflineEarning : MonoBehaviour
{
    [SerializeField]Upgrade_SO Upgrade_SO;
    public static event Action offlineEarning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Upgrade_SO.isPurchased == true)
        {
            CheckForOfflineEarning();
        }
    }

    public void CheckForOfflineEarning()
    {
        offlineEarning?.Invoke();
    }



}
