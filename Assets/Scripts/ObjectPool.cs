using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public int initialPoolSize = 10;

    private List<GameObject> poolObjects;

    private void Awake()
    {
        poolObjects = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            foreach (GameObject prefab in obstaclePrefabs)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                poolObjects.Add(obj);
            }
        }
    }

    public GameObject GetObject(bool isAvoidObstacle)
    {
        foreach (GameObject obj in poolObjects)
        {
            if (!obj.activeInHierarchy)
            {
                if ((isAvoidObstacle && obj.GetComponent<AvoidObstacle>() != null) ||
                    (!isAvoidObstacle && obj.GetComponent<CollectibleObstacle>() != null))
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
        }

        foreach (GameObject prefab in obstaclePrefabs)
        {
            if ((isAvoidObstacle && prefab.GetComponent<AvoidObstacle>() != null) ||
                (!isAvoidObstacle && prefab.GetComponent<CollectibleObstacle>() != null))
            {
                GameObject newObj = Instantiate(prefab);
                newObj.SetActive(true);
                poolObjects.Add(newObj);
                return newObj;
            }
        }

        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}