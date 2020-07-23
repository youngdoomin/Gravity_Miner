using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] tilePooledObjects;
    public GameObject[] pooledObjects;

    public GameObject enemy;
    public int spawnCt;

    private List<GameObject> oldTileObj = new List<GameObject>();
    private List<GameObject> oldObj = new List<GameObject>();
    public Transform spawnPos;
    public float xAxisRandom;
    public float tileDelay;
    public float delay;

    int poolCt;
    private void Awake()
    {
        Instance = this;

    }
    void Start()
    {
        for(int i = 0; i < spawnCt; i++)
        {
            var localObj = Instantiate(enemy, transform.position, Quaternion.identity);
            localObj.transform.parent = spawnPos.transform;
            localObj.SetActive(false);
        }

        for (int i = 0; i < tilePooledObjects.Length; i++)
        {
            var localObj = Instantiate(tilePooledObjects[i], transform.position, Quaternion.identity);
            localObj.transform.parent = spawnPos.transform;
            localObj.SetActive(false);
        }
        GetRandomPooledObject();
        GetPooledObject();
    }

    public GameObject GetRandomPooledObject()
    {
        int randomIndex = Random.Range(0, tilePooledObjects.Length);
        Debug.Log("Index" + randomIndex);
        GameObject go = tilePooledObjects[randomIndex];
        GameObject[] allChildren = GetComponentsInChildren<GameObject>();
        foreach (GameObject child in allChildren)
        {
            child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            child.GetComponent<BoxCollider2D>().enabled = true;
        }
        go.gameObject.transform.position = new Vector3(transform.position.x, -spawnPos.position.y, transform.position.z);
        go.SetActive(true);
        oldTileObj.Add(go);

        return null;
    }
    public void TileDestroyed()
    {
        oldTileObj[0].SetActive(false);
        oldTileObj.RemoveAt(0);
        Invoke("GetRandomPooledObject", tileDelay);
        //GetRandomPooledObject();
    }

    public GameObject GetPooledObject()
    {
        Debug.Log("실행");
        float xAxis = Random.Range(-xAxisRandom, xAxisRandom);
        GameObject go = pooledObjects[poolCt];
        poolCt++;
        go.gameObject.transform.position = new Vector3(xAxis, -spawnPos.position.y, transform.position.z);
        go.SetActive(true);
        oldObj.Add(go);

        return null;
    }

    public void Destroyed()
    {
        oldObj[0].SetActive(false);
        oldObj.RemoveAt(0);
        Invoke("GetPooledObject", delay);
        //GetPooledObject();
    }



}
