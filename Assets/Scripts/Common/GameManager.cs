﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] jamBlocks;
    List<int> list = new List<int>() { 0, 1, 2, 3, 4 };
    private int bf;
    GameObject UIfinder;

    public GameObject[] tilePooledObjects;
    public float tilediff = 500;
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
    Queue<GameObject> oldJamObj = new Queue<GameObject>();
    public Transform tileSpawnPos;
    public Transform enemySpawnPos;
    public Transform itemSpawnPos;
    public Transform jamSpawnPos;
    public float xAxisRandom;
    public float tileDelay;
    public float enemyDelay;
    public float itemDelay;
    public float jamDelay;

    float xAxis;
    int poolCt;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UIfinder = GameObject.Find("JamManager");

        Debug.LogFormat("타일 딜레이 : {0}, 딜레이 {1}", tileDelay, enemyDelay);


        for (int i = 0; i < spawnCt; i++) // 인스펙터에 있는 적 생성
        {
            onlyObj = Instantiate(enemy, transform.position, Quaternion.identity);
            onlyObj.transform.parent = enemySpawnPos.transform;
            onlyObj.SetActive(false);
        }
        GetPooledObject(enemy, enemySpawnPos.transform);

        for (int i = 0; i < jamBlocks.Length; i++) // 인스펙터에 있는 젬 생성
        {
            onlyObj = Instantiate(jamBlocks[i], transform.position, Quaternion.identity);
            onlyObj.transform.parent = jamSpawnPos.transform;
            onlyObj.SetActive(false);
        }
        int jamRandom = Random.Range(0, jamBlocks.Length);
        //GetJamPooledObject(jamBlocks[jamRandom], jamSpawnPos.transform);

        for (int i = 0; i < pooledObjects.Length; i++) // 인스펙터에 있는 아이템 생성
        {
            onlyObj = Instantiate(pooledObjects[i], transform.position, Quaternion.identity);
            onlyObj.transform.parent = itemSpawnPos.transform;
            onlyObj.SetActive(false);
        }
        int randomIndex = Random.Range(0, pooledObjects.Length);
        //GetPooledObject(pooledObjects[randomIndex], itemSpawnPos.transform);

        for (int i = 0; i < tilePooledObjects.Length; i++)
        {
            spawnedTile = Instantiate(tilePooledObjects[i], transform.position, Quaternion.identity);
            spawnedTile.transform.parent = tileSpawnPos.transform;
            spawnedTile.SetActive(false);
        } // 인스펙터에 있는 타일 생성
        GetRandomPooledObject();

        xAxis = Random.Range(-xAxisRandom, xAxisRandom);
    }
    
    void Update()
    {
        if (list.Count == 0 && JamUI.collect == false) // 젬을 다 모았을 경우
        {
            UIfinder.SendMessage("Collect");
            list = new List<int> { 0, 1, 2, 3, 4 }; // 초기화
            //GetJamPooledObject();
        }

    }


    void Pickup(int jamID)
    {

        foreach (int num in list) // 리스트에 있는 모든 수
        {
            if (num == jamID) // 값과 인덱스가 일치하면
            {
                list.Remove(num); // 리스트에서 삭제
                JamUI.repeat = false;
                Debug.Log("중복 아님");
            }
            else
            {
                JamUI.repeat = true;
                Debug.Log("중복");
            }
        }
    }

    public GameObject GetJamPooledObject(GameObject obj, Transform tr)
    {
    RESTART:

        int c = Random.Range(0, list.Count); // 리스트 중에 랜덤으로 고름

        if (c == bf && list.Count != 1) // 전에 나왔고, 젬이 한 개 이상일 경우
        {
            goto RESTART; // 다시 랜덤돌림
        }
        else
        {
            bf = c; // 다음에 나올 젬과 중복 감지하게 함

            GetPooledObject(obj, tr);
        }
        return null;
    }


    public GameObject GetRandomPooledObject()
    {
        int randomIndex = 0;

        if (Score.scoreCt > 2 * tilediff)
        {
            randomIndex = Random.Range(tilePooledObjects.Length - tilePooledObjects.Length / 3, tilePooledObjects.Length - 1);
        }
        else if (2 * tilediff > Score.scoreCt && Score.scoreCt > tilediff)
        {
            randomIndex = Random.Range(tilePooledObjects.Length / 3, tilePooledObjects.Length / 3 * 2 - 1);
        }
        else if (Score.scoreCt < tilediff)
        {
            randomIndex = Random.Range(0, tilePooledObjects.Length / 3 - 1);
        }
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
        ;
        Transform go = tr.GetChild(poolCt);
        poolCt++;
        if(poolCt == tr.transform.childCount)
        {
            poolCt = 0;
        }
        go.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        go.GetComponent<BoxCollider2D>().enabled = true;
        go.gameObject.transform.position = new Vector3(xAxis, -tr.position.y, transform.position.z);
        go.gameObject.SetActive(true);
        Debug.Log(go.gameObject);
        if(tr.gameObject.tag == "Enemy")
        {
            oldEnemyObj.Enqueue(go.gameObject);

        }
        else if(tr.gameObject.tag == "Platform_jam" || tr.tag == "jam")
        {
            oldJamObj.Enqueue(go.gameObject);
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
        else if(obj.tag == "Platform_jam" || obj.tag == "jam")
        {
            int randomIndex = Random.Range(0, jamBlocks.Length);
            oldJamObj.Peek().SetActive(false);
            oldJamObj.Dequeue();
            StartCoroutine(wait(itemDelay));
            GetJamPooledObject(jamBlocks[randomIndex], jamSpawnPos.transform);
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
