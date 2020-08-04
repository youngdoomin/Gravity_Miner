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
    Queue<GameObject> oldItemObj = new Queue<GameObject>();
    Queue<GameObject> oldEnemyObj = new Queue<GameObject>();
    public Transform tileSpawnPos;
    public Transform enemySpawnPos;
    public Transform itemSpawnPos;
    public float xAxisRandom;
    public float tileDelay;
    public float enemyDelay;
    public float itemDelay;

    int poolCt;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Debug.LogFormat("타일 딜레이 : {0}, 딜레이 {1}", tileDelay, enemyDelay);
        for(int i = 0; i < spawnCt; i++)
        {
            onlyObj = Instantiate(enemy, transform.position, Quaternion.identity);
            onlyObj.transform.parent = enemySpawnPos.transform;
            onlyObj.SetActive(false);
        }
        GetPooledObject(enemy, enemySpawnPos.transform);

        for (int i = 0; i < pooledObjects.Length; i++)
        {
            onlyObj = Instantiate(pooledObjects[i], transform.position, Quaternion.identity);
            onlyObj.transform.parent = itemSpawnPos.transform;
            onlyObj.SetActive(false);
        }
        int randomIndex = Random.Range(0, pooledObjects.Length);
        GetPooledObject(pooledObjects[randomIndex], itemSpawnPos.transform);

        for (int i = 0; i < tilePooledObjects.Length; i++)
        {
            spawnedTile = Instantiate(tilePooledObjects[i], transform.position, Quaternion.identity);
            spawnedTile.transform.parent = tileSpawnPos.transform;
            spawnedTile.SetActive(false);
        }
        GetRandomPooledObject();
        
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
        oldTileObj.Peek().SetActive(false);
        oldTileObj.Dequeue();
        Invoke("GetRandomPooledObject", tileDelay);
        //GetRandomPooledObject();
    }
    
    public GameObject GetPooledObject(GameObject obj, Transform tr)
    {
        Debug.Log(tr);
        float xAxis = Random.Range(-xAxisRandom, xAxisRandom);
        Transform go = tr.GetChild(poolCt);
        poolCt++;
        if(poolCt == tr.transform.childCount)
        {
            poolCt = 0;
        }
        go.gameObject.transform.position = new Vector3(xAxis, -tr.position.y, transform.position.z);
        go.gameObject.SetActive(true);
        Debug.Log(go.gameObject);
        if(tr.gameObject.tag == "Enemy")
        {
            oldEnemyObj.Enqueue(go.gameObject);

        }
        else
        {
            oldItemObj.Enqueue(go.gameObject);
        }

        return null;
    }
    
    public void ObjDestroy(GameObject obj)
    {
        if(obj == enemy)
        {
            oldEnemyObj.Peek().SetActive(false);
            oldEnemyObj.Dequeue();
            StartCoroutine(wait(enemyDelay));
            GetPooledObject(enemy, enemySpawnPos.transform);
        }
        else
        {
            int randomIndex = Random.Range(0, pooledObjects.Length);
            oldItemObj.Peek().SetActive(false);
            oldItemObj.Dequeue();
            StartCoroutine(wait(itemDelay));
            GetPooledObject(pooledObjects[randomIndex], itemSpawnPos.transform);
        }
        //GetPooledObject();
    }
    
    IEnumerator wait(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

}
