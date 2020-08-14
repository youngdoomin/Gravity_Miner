using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapSpawner : MonoBehaviour
{
    public float tilediff = 500;
    public GameObject[] objects;             //넣을 오브젝트들
    public float spawnDelay = 6f;            //딜레이
    public GameObject grid;                  //그리드
    private GameObject gridChild;
    public static int tileCt = 0;
    public float tileRange = 50.0f;


    void Start()
    {
        tileCt = 0;
        StartCoroutine(AfterTilemapSpawn(-5));
    }

    private void Update()
    {
        if(tileCt == 0)
        {
            StartCoroutine(waitSpawn());
        }
    }




    IEnumerator AfterTilemapSpawn(float tilemapY)
    {
        yield return new WaitForSeconds(spawnDelay);
        tileCt++;
        Vector3 spawnPos = new Vector3(0, tilemapY - tileRange, 0);

        TilemapSpawner tile = new TilemapSpawner();
        
        if (Score.scoreCt > 2 * tilediff)
        {
            //게임옵젝생성(오브젝트, 위치, 회전)  identity 기본값
            gridChild = Instantiate(objects[UnityEngine.Random.Range(objects.Length - objects.Length / 3 , objects.Length - 1)], spawnPos, Quaternion.identity); 
        }
        else if(2 * tilediff > Score.scoreCt && Score.scoreCt > tilediff)
        {
            //게임옵젝생성(오브젝트, 위치, 회전)  identity 기본값
            gridChild = Instantiate(objects[UnityEngine.Random.Range(objects.Length / 3, objects.Length / 3 * 2 - 1)], spawnPos, Quaternion.identity); 
        }
        else if(Score.scoreCt < tilediff)
        {
            //게임옵젝생성(오브젝트, 위치, 회전) identity 기본값
            gridChild = Instantiate(objects[UnityEngine.Random.Range(0, objects.Length / 3 - 1)], spawnPos, Quaternion.identity); 
        }
        gridChild.transform.SetParent(grid.transform);
    }
    

    IEnumerator waitSpawn()
    {
        yield return new WaitForSeconds(4f);
        if(tileCt == 0)
        {
            tileCt++;
            AfterTilemapSpawn(-5);
        }
    }


}