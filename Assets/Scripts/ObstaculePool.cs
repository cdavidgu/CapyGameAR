using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculePool : MonoBehaviour
{
    // Start is called before the first frame update
    public static ObstaculePool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject jaguarObjectToPool;
    public GameObject alligatorObjectToPool;
    public GameObject rewardObjectToPool;
    int amountToPool = 3;


    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            InstantiatePoolObject(jaguarObjectToPool);
            InstantiatePoolObject(alligatorObjectToPool);
            InstantiatePoolObject(rewardObjectToPool);
        }
        InstantiatePoolObject(rewardObjectToPool);
    }

    void InstantiatePoolObject(GameObject goPrefab)
    {
        GameObject tmp;
        tmp = Instantiate(goPrefab);
        tmp.transform.SetParent(this.transform);
        tmp.transform.localScale = tmp.transform.localScale * 0.07f;
        tmp.SetActive(false);
        pooledObjects.Add(tmp);
    }
    public GameObject GetPooledObject()
    {
        int randomIndex = Random.Range(0, pooledObjects.Count);
        if (!pooledObjects[randomIndex].activeInHierarchy)
        {
            return pooledObjects[randomIndex];
        }
        return null;
    }

    public void ResetPool()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
                pooledObjects[i].SetActive(false);
        }
    }
}
