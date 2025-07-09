using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private BoxCollider centerCollider;
    [SerializeField] private float spawnDistance = 3f; // Distance from collider edge

    [SerializeField] Upgrade_SO autoCollectSO;


    private void Start()
    {
        ObjectSpawn(autoCollectSO.upgradeLevel);
    }
    private void OnEnable()
    {
        ShopMenu.autoClickerUpgradeEvent += SpawnLogic;
    }

    private void OnDisable()
    {
        ShopMenu.autoClickerUpgradeEvent -= SpawnLogic;
    }

    public void ObjectSpawn(double value)
    {
        for (int i = 0; i < value; i++)
        {
            SpawnLogic();
        }
    }
    public void SpawnLogic(double value = 0)
    {
        Vector3 center = centerCollider.bounds.center;

        // Random angle in circle (XZ plane)
        float angle = Random.Range(0f, 360f);
        float radius = Mathf.Max(centerCollider.bounds.extents.x, centerCollider.bounds.extents.z) + spawnDistance;

        Vector3 offset = new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            0,
            Mathf.Sin(angle * Mathf.Deg2Rad)
        ) * radius;

        Vector3 spawnPos = new Vector3(center.x + offset.x, centerCollider.transform.position.y, center.z + offset.z);
       Instantiate(objectToSpawn, spawnPos, Quaternion.identity, transform);
       
       



    }
}
