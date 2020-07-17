using UnityEngine;

public class StartManager : MonoBehaviour
{
    public int flagNumber = 0;
    public GameObject[] startPlatform;  // 시작 발판
    public GameObject[] UI;
    public GameObject startBg; // 시작배경
    public GameObject text;

    [Header("scripts")]
    public Scrolling scrolling;
    public TilemapSpawner tilemapspawner;
    public HPManager hPManager;
    public EnemySpawner enemySpawner;
    public Combo combo;
    public Score score;

    public float BgSpeed = 5;
    
    private void Awake()
    {

        scrolling.enabled = false;
        tilemapspawner.enabled = false;
        hPManager.enabled = false;
        enemySpawner.enabled = false;
        combo.enabled = false;
        score.enabled = false;
        flagNumber = 0;
        
        foreach(var ui in UI)
        {
            ui.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flagNumber < 5)
        {
            StartFlag();
        }
    }

    void StartFlag()
    {

        switch (flagNumber)
        {

            case 0: 	//시작 트리거

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Destroy(text.gameObject);
                    scrolling.enabled = true;
                    Debug.Log("game start!");
                    flagNumber++;
                }
                break;

            case 1: // 발판 파괴
                foreach (var platform in startPlatform)
                {
                    Destroy(platform.gameObject);
                }

                flagNumber++;

                break;

            case 2: //배경 변경
            if(startBg.transform.position.y < Camera.main.transform.position.y + 50) 
            {
                    startBg.transform.position += Vector3.up * Time.deltaTime * BgSpeed; 
            }
                else
                {
                    flagNumber++;
                }
                break;

            case 3: // 오브젝트 활성화
                foreach (var ui in UI)
                {
                    ui.SetActive(true);
                }
                flagNumber++;
                break;

            case 4: //스크립트 활성화

                tilemapspawner.enabled = true;
                hPManager.enabled = true;
                enemySpawner.enabled = true;
                combo.enabled = true;
                score.enabled = true;
                scrolling.enabled = true;

                flagNumber++;
                break;
                
        }
    }
}
