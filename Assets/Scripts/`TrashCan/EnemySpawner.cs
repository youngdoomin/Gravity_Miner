using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float EnDelay;
    public Collider2D[] tiles;
    public GameObject[] EnemyObjects;          //넣을 오브젝트들 
    public GameObject EnemySp;
    private GameObject EnemyChild;

    public static int EnemyCt = 0;
    public float spawnY;
    //    public float spawnTime = 6f;          //딜레이  
    void Start()
    {
        EnemyCt = 0;
        StartCoroutine(EnemySpawn(-20));
        //        InvokeRepeating("EnemySpawn", 1, spawnTime);
        //spawnY = Random.Range(25.0f, 30.0f);
    }

    IEnumerator EnemySpawn(float enemyY)
    {
        yield return new WaitForSeconds(EnDelay);
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        bool spawnPoss = false;
        int safetyNet = 0;

        while (!spawnPoss)
        {
            float spawnPosX = Random.Range(-8.5f, 8.5f);      //최솟값 최대값 사이 값 도출(x축)
            float spawnPosY = enemyY - spawnY;    //최솟값 최대값 사이 값 도출(y축)
            spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
            spawnPoss = PreventSpawnOverlap(spawnPosition);

            if (spawnPoss)
            {
                break;
            }

            safetyNet++;
            if (safetyNet > 50)
            {
                Debug.Log("too many attempts");
                break;

            }
        }
        EnemyCt++;

        EnemyChild = Instantiate(EnemyObjects[UnityEngine.Random.Range(0, EnemyObjects.Length - 1)], spawnPosition, Quaternion.identity); //게임옵젝생성(오브젝트, 위치, 회전)       identity 기본값
        EnemyChild.transform.SetParent(EnemySp.transform);

    }
    private void Update()
    {
        if (EnemyCt <= 0)
        {
            EnemySpawn(-15);
        }
        Debug.Log(EnemyCt);
    }
    bool PreventSpawnOverlap(Vector3 spawnPos)
    {
        tiles = Physics2D.OverlapCircleAll(transform.position, 2);

        for (int i = 0; i < tiles.Length; i++)
        {
            Vector3 centerPos = tiles[i].bounds.center;
            float width = tiles[i].bounds.extents.x;
            float height = tiles[i].bounds.extents.y;

            float leftExtent = centerPos.x - width;
            float rightExtent = centerPos.y - height;
            float lowerExtent = centerPos.y - height;
            float upperExtent = centerPos.y + height;

            if (spawnPos.x >= leftExtent && spawnPos.x <= rightExtent)
            {
                if (spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent)
                {
                    return false;
                }
            }

        }
        return true;

    }

}