using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public bool willGrow = true;

    public List<GameObject>[] pooledObjects;

    void Start()
    {
        // instantiate our pool (empty)
        pooledObjects = new List<GameObject>[platformPrefabs.Length];
        for (int i = 0; i < platformPrefabs.Length; i++)
        {
            pooledObjects[i] = new List<GameObject>();
        }
    }

    public GameObject GetPooledObject()
    {
        int randomIndex = Random.Range(0, pooledObjects.Length);

        for (int i = 0; i < pooledObjects[randomIndex].Count; i++)
        {
            GameObject go = pooledObjects[randomIndex][i];

            // I commented this out because I believe this should never happen,
            // as this would mean that you destroy items from the pool which is
            // something that you want to prevent when you use a pool...

            //if (go == null)
            //{
            //    go = (GameObject)Instantiate(platformPrefabs[randomIndex]);
            //    go.SetActive(false);
            //    pooledObjects[randomIndex][i] = go;
            //    return go;
            //}

            if (!go.activeInHierarchy)
            {
                return go;
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(platformPrefabs[randomIndex]);
            pooledObjects[randomIndex].Add(obj);
            return obj;
        }

        return null;
    }

}