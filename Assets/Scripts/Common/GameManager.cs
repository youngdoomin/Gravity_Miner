using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] tilePooledObjects;
    private GameObject spawnedTile;
    public GameObject[] pooledObjects;
    private GameObject onlyObj;

    public GameObject enemy;
    public int spawnCt;

    //private List<GameObject> oldTileObj = new List<GameObject>();
    //private List<GameObject> oldObj = new List<GameObject>();
    Queue<GameObject> oldTileObj = new Queue<GameObject>();
    Queue<GameObject> oldObj = new Queue<GameObject>();
    public Transform tileSpawnPos;
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
        Debug.LogFormat("타일 딜레이 : {0}, 딜레이 {1}", tileDelay, delay);
        for(int i = 0; i < spawnCt; i++)
        {
            onlyObj = Instantiate(enemy, transform.position, Quaternion.identity);
            onlyObj.transform.parent = spawnPos.transform;
            onlyObj.SetActive(false);
        }

        for (int i = 0; i < tilePooledObjects.Length; i++)
        {
            spawnedTile = Instantiate(tilePooledObjects[i], transform.position, Quaternion.identity);
            spawnedTile.transform.parent = tileSpawnPos.transform;
            spawnedTile.SetActive(false);
        }
        GetRandomPooledObject();
        GetPooledObject();
    }

    public GameObject GetRandomPooledObject()
    {
        int randomIndex = Random.Range(0, tilePooledObjects.Length);
        Debug.Log("Index" + randomIndex);

        Transform go = tileSpawnPos.transform.GetChild(randomIndex);

        for (int i = 0; i < go.transform.childCount; i++)
        {
            go.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            go.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
        

        }
        

        go.gameObject.transform.position = new Vector3(transform.position.x, -tileSpawnPos.position.y, transform.position.z);
        go.gameObject.SetActive(true);
        oldTileObj.Enqueue(go.gameObject);
        
        return null;
    }
    public void TileDestroy()
    {
        oldTileObj.Dequeue();
        Invoke("GetRandomPooledObject", tileDelay);
        oldTileObj.Peek().SetActive(false);
        //GetRandomPooledObject();
    }
    
    public GameObject GetPooledObject()
    {
        Debug.Log("실행");
        float xAxis = Random.Range(-xAxisRandom, xAxisRandom);
        Transform go = spawnPos.transform.GetChild(poolCt);
        poolCt++;
        if(poolCt > spawnPos.transform.childCount)
        {
            poolCt = 0;
        }
        go.gameObject.transform.position = new Vector3(xAxis, -spawnPos.position.y, transform.position.z);
        go.gameObject.SetActive(true);
        oldObj.Enqueue(go.gameObject);

        return null;
    }
    
    public void ObjDestroy()
    {
        oldObj.Peek().SetActive(false);
        oldObj.Dequeue();
        Invoke("GetPooledObject", delay);
        //GetPooledObject();
    }
    


}
