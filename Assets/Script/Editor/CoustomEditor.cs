using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PlayerManager))]
public class CoustomEditor : EditorWindow
{
    private int coinsToAdd = 0;
    List<Upgrade_SO> upgradeSO = new();
    GameManager gameManager;

    [MenuItem("Window/Custom/Coin Giver Panel")]
    public static void ShowWindow()
    {
        GetWindow<CoustomEditor>("Coin Giver");
    }

    private void OnGUI()
    {
        GUILayout.Label("Coin Giver", EditorStyles.boldLabel);

        coinsToAdd = EditorGUILayout.IntField("Coins to Add", coinsToAdd);

        gameManager = (GameManager)EditorGUILayout.ObjectField("Game Manager", gameManager, typeof(GameManager), true);
        if (GUILayout.Button("Give Coins"))
        {
            if (Application.isPlaying)
            {
                if (PlayerManager.Instance != null)
                {
                    PlayerManager.Instance.current_Currency = coinsToAdd;
                }
                else
                {
                    Debug.LogWarning("PlayerManager.Instance is null.");
                }
            }
            else
            {
                Debug.LogWarning("You must be in Play Mode to give coins.");
            }
        }
        for (int i = 0; i < upgradeSO.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("box");

            upgradeSO[i] = (Upgrade_SO)EditorGUILayout.ObjectField($"Reward {i + 1}", upgradeSO[i], typeof(Upgrade_SO), false);

            if (GUILayout.Button("Remove", GUILayout.Width(70)))
            {
                upgradeSO.RemoveAt(i);
                break;
            }

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add New Slot"))
        {
            upgradeSO.Add(null);
        }
        if(GUILayout.Button("Reset All Upgrades"))
        {
            foreach (var upgrade in upgradeSO)
            {
                if (upgrade != null)
                {
                    upgrade.Reset();
                    gameManager.AutoMultiplier = 1;
                    gameManager.offlineMultiplier = 1;
                    gameManager.TapMultiplier = 1;
                    Debug.Log("All upgrades have been reset.");
                }
            }
            
        }


       
    }
}
