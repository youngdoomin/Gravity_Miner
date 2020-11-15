using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public bool shake, shieldOn, screenFilter, untouchable, GroundDam, kill, killLoop, comboActive,
        reaction, delay, collect, collectTxt, repeat, scorecap, scoreActive, isPaused, tileBreak,
        getJam;
    [HideInInspector]
    public float energy = PGravity.fenergy, gravityVal, sp, waitTime, scoreCt;

    public int life, uiJamCt, jamCt;
    public float speedLock = 40.0f;
    public Image[] cJamArray;
    public Sprite cJamUI;

    Collider2D[] blocks;
    public GameObject[] jamBlocks;
    List<int> list = new List<int>() { 0, 1, 2, 3, 4 };
    private int bf;
    GameObject UIfinder;
    GameObject PlayerFinder;

    public GameObject[] tilePooledObjects;
    public float tilediff = 500;
    private GameObject spawnedTile;
    public GameObject[] pooledObjects;
    private GameObject onlyObj;

    public GameObject enemy;
    public int spawnCt;

    public Transform tileSpawnPos;
    public Transform enemySpawnPos;
    public Transform itemSpawnPos;
    public Transform jamSpawnPos;
    public float xAxisRandom;
    public float tileDelay;
    public float enemyDelay;
    public float itemDelay;
    public float jamDelay;
    private Animator animator;
    public RuntimeAnimatorController settingControl;
    public RuntimeAnimatorController pauseControl;

    float xAxis;
    int poolCt;

    Text full;
    Text Windowed;
    Color black = Color.black;
    Color gray = Color.gray;
    Resolution resolution;

    private void Awake()
    {
        Instance = this;

        resolution = Screen.currentResolution;
    }
    /*  더이상 지원하지 않는 메소드
    private void OnLevelWasLoaded(int level)
    {
        animator = GameObject.Find("Setting_B").GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "Flower_Rain")
        {
            animator.runtimeAnimatorController = pauseControl;
        }
        else
        {
            animator.runtimeAnimatorController = settingControl;
        }
    }
    */
    private void OnEnable()
    {
        SceneManager.sceneLoaded += GameManager_sceneloaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= GameManager_sceneloaded;
    }

    private void GameManager_sceneloaded(Scene scene, LoadSceneMode mode)
    {

        animator = GameObject.Find("Setting_B").GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "Flower_Rain")
        {
            animator.runtimeAnimatorController = pauseControl;
        }
        else
        {
            animator.runtimeAnimatorController = settingControl;
        }
    }
    void Start()
    {

        //Screen.SetResolution(Screen.width, Screen.height, true);
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        if (SceneManager.GetActiveScene().name == "Flower_Rain")
        {
            tileSpawnPos = GameObject.Find("Block").transform;
            enemySpawnPos = GameObject.Find("Enemy").transform;
            itemSpawnPos = GameObject.Find("Item").transform;
            jamSpawnPos = GameObject.Find("Jam").transform;
            UIfinder = GameObject.Find("JamManager");
            PlayerFinder = GameObject.Find("Player");

            Debug.LogFormat("타일 딜레이 : {0}, 딜레이 {1}", tileDelay, enemyDelay);

            for (int i = 0; i < spawnCt; i++) // 인스펙터에 있는 적 생성
            {
                onlyObj = Instantiate(enemy, transform.position, Quaternion.identity);
                onlyObj.transform.parent = enemySpawnPos.transform;
                onlyObj.SetActive(false);
            }
            GetPooledObject(false, enemySpawnPos.transform);

            for (int i = 0; i < jamBlocks.Length; i++) // 인스펙터에 있는 젬 생성
            {
                onlyObj = Instantiate(jamBlocks[i], transform.position, Quaternion.identity);
                onlyObj.transform.parent = jamSpawnPos.transform;
                onlyObj.SetActive(false);
            }
            GetJamPooledObject(jamSpawnPos.transform);

            for (int i = 0; i < pooledObjects.Length; i++) // 인스펙터에 있는 아이템 생성
            {
                onlyObj = Instantiate(pooledObjects[i], transform.position, Quaternion.identity);
                onlyObj.transform.parent = itemSpawnPos.transform;
                onlyObj.SetActive(false);
            }
            GetPooledObject(true, itemSpawnPos);

            for (int i = 0; i < tilePooledObjects.Length; i++)
            {
                spawnedTile = Instantiate(tilePooledObjects[i], transform.position, Quaternion.identity);
                spawnedTile.transform.parent = tileSpawnPos.transform;
                spawnedTile.SetActive(false);
            } // 인스펙터에 있는 타일 생성
            GetRandomPooledObject();


        }

    }

    void Update()
    {

        if (list.Count == 0 && collect == false) // 젬을 다 모았을 경우
        {
            UIfinder.SendMessage("Collect");
            list = new List<int> { 0, 1, 2, 3, 4 }; // 초기화
            //GetJamPooledObject();
        }
    }

    public void WindowedScreen(bool isActive)
    {
        full = GameObject.Find("FullScreen").GetComponent<Text>();
        Windowed = GameObject.Find("Windowed").GetComponent<Text>();
        if (isActive)
        {
            Screen.SetResolution(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, false);
            Windowed.color = black;
            full.color = gray;
        }
        else
        {
            Screen.SetResolution(resolution.width, resolution.height, true);
            Windowed.color = gray;
            full.color = black;
        }
    }

    void Pickup(int jamID)
    {
        foreach (int num in list) // 리스트에 있는 모든 수
        {

            if (num == jamID) // 값과 인덱스가 일치하면
            {
                list.Remove(num); // 리스트에서 삭제
                repeat = false;
                Debug.Log("중복 아님");
            }
            else
            {
                repeat = true;
                Debug.Log("중복");
            }
        }
    }

    public GameObject GetJamPooledObject(Transform tr)
    {
    RESTART:

        int c = Random.Range(0, list.Count); // 리스트 중에 랜덤으로 고름

        if (c == bf && list.Count != 1) // 전에 나왔고, 젬이 한 개 이상일 경우
        { goto RESTART; }// 다시 랜덤돌림
        else
        {
            bf = c; // 다음에 나올 젬과 중복 감지하게 함
            GetPooledObject(true, tr);
        }
        return null;
    }


    public GameObject GetRandomPooledObject()
    {
        int count = 0;
        int randomIndex = 0;

        for (int i = 0; i < tileSpawnPos.childCount; i++)
        {
            if (tileSpawnPos.GetChild(i).gameObject.activeInHierarchy)
            {
                count++;
            }
        }

        Debug.Log(count);

        if (count > 2) { return null; };

        while (true)
        {
            if (scoreCt > 2 * tilediff)
            {
                randomIndex = Random.Range(tilePooledObjects.Length - tilePooledObjects.Length / 3, tilePooledObjects.Length - 1);
            }
            else if (2 * tilediff > scoreCt && scoreCt > tilediff)
            {
                randomIndex = Random.Range(tilePooledObjects.Length / 3, tilePooledObjects.Length / 3 * 2 - 1);
            }
            else if (scoreCt < tilediff)
            {
                randomIndex = Random.Range(0, tilePooledObjects.Length / 3 - 1);
            }
            Debug.Log("Index" + randomIndex);

            if (!tileSpawnPos.transform.GetChild(randomIndex).gameObject.activeInHierarchy) { break; }
        }

        Transform go = tileSpawnPos.transform.GetChild(randomIndex);

        go.gameObject.transform.position = new Vector3(transform.position.x, PlayerFinder.transform.position.y - 30, transform.position.z);
        go.gameObject.SetActive(true);

        return null;
    }


    public GameObject GetPooledObject(bool isRandom, Transform tr)
    {
        Debug.Log(tr);
        xAxis = Random.Range(-xAxisRandom, xAxisRandom);

        if (isRandom)
        {
            int randomIndex = Random.Range(0, tr.transform.childCount);
            int tryCt = 0;
            while (tr.GetChild(randomIndex).gameObject.activeInHierarchy == true)
            {
                randomIndex = Random.Range(0, tr.transform.childCount);

                tryCt++;
                if (tryCt > 50)
                {
                    Debug.Log("too many attempts");
                    for (int i = 0; i < tr.childCount; i++)
                    { tr.GetChild(i).gameObject.SetActive(false); }
                }
            }
            poolCt = randomIndex;
        }
        else if (poolCt >= tr.transform.childCount)
        { poolCt = 0; }

        bool spawnPoss = false;
        int safetyNet = 0;

        while (!spawnPoss)
        {
            float spawnPosY = PlayerFinder.transform.position.y - 40 - Random.Range(0, 8);
            Vector3 spawnPosition = new Vector3(xAxis, spawnPosY, 0);
            spawnPoss = PreventSpawnOverlap(spawnPosition);

            if (spawnPoss)
            { break; }

            safetyNet++;
            if (safetyNet > 50)
            {
                Debug.Log("too many attempts");
                break;
            }
        }
        Transform go = tr.GetChild(poolCt);
        go.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        go.GetComponent<BoxCollider2D>().enabled = true;
        go.gameObject.transform.position = new Vector3(xAxis, PlayerFinder.transform.position.y - 40 - xAxis, transform.position.z);
        go.gameObject.SetActive(true);
        Debug.Log(go.gameObject);
        if (tr.gameObject.tag == "Enemy")
        { poolCt++; }

        return null;
    }

    public void ObjDestroy(string str)
    {
        if (str == "enemy")
        { GetPooledObject(false, enemySpawnPos.transform); }
        else if (str == "jam")
        {
            StartCoroutine(wait(itemDelay));
            GetJamPooledObject(jamSpawnPos.transform);
        }

        else if (str == "item")
        {
            StartCoroutine(wait(itemDelay));
            GetPooledObject(true, itemSpawnPos.transform);
        }
    }
    public void TileDestroy()
    { Invoke("GetRandomPooledObject", tileDelay); }

    IEnumerator wait(float sec)
    { yield return new WaitForSeconds(sec); }

    bool PreventSpawnOverlap(Vector3 spawnPos)
    {
        blocks = Physics2D.OverlapCircleAll(transform.position, 4);

        for (int i = 0; i < blocks.Length; i++)
        {
            Vector3 centerPos = blocks[i].bounds.center;
            float width = blocks[i].bounds.extents.x;
            float height = blocks[i].bounds.extents.y;

            float leftExtent = centerPos.x - width;
            float rightExtent = centerPos.y - height;
            float lowerExtent = centerPos.y - height;
            float upperExtent = centerPos.y + height;

            if (spawnPos.x >= leftExtent && spawnPos.x <= rightExtent)
            {
                if (spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent)
                { return false; }
            }
        }
        return true;
    }
}
