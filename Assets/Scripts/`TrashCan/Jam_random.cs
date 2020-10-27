using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jam_random : MonoBehaviour
{

    public GameObject[] jamBlocks;
    public GameObject[] itemBlocks;
    private GameObject jB;
    List<int> list = new List<int>() { 0, 1, 2, 3, 4 };
    private GameObject gridObj;
    private int bf;
    GameObject UIfinder;
    // Start is called before the first frame update
    void Start()
    {
        UIfinder = GameObject.Find("JamManager");
        gridObj = GameObject.Find("Grid");
        StartCoroutine(JamSpawnLoop());
        StartCoroutine(ItemSpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (list.Count == 0 && GameManager.Instance.collect == false) // 젬을 다 모았을 경우
        {
            UIfinder.SendMessage("Collect");
            list = new List<int> { 0, 1, 2, 3, 4 }; // 초기화
            StartCoroutine(JamSpawnLoop());
        }

    }


    void Pickup(int jamID)
    {

        foreach (int num in list) // 리스트에 있는 모든 수
        {
            if (num == jamID) // 값과 인덱스가 일치하면
            {
                list.Remove(num); // 리스트에서 삭제
                GameManager.Instance.repeat = false;
                Debug.Log("중복 아님");
            }
            else
            {
                GameManager.Instance.repeat = true;
                Debug.Log("중복");
            }
        }
    }

    IEnumerator JamSpawnLoop()
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


            Vector3 JamPos = new Vector3(Random.Range(-10.5f, 10.5f), -25, 0); // 위치 지정
            jB = Instantiate(jamBlocks[Random.Range(0, jamBlocks.Length)], JamPos, Quaternion.identity); // 랜덤이고 중복아닌 리스트 값을 스폰할 인덱스에 넣고 스폰
            jB.transform.SetParent(gridObj.transform); // 그리드 내부에 들어가게 함
            yield return new WaitForSeconds(3);

            StartCoroutine(JamSpawnLoop()); // 반복 실행(리스트가 없어질 때까지)
        }
    }

    IEnumerator ItemSpawnLoop()
    {
        Vector3 ItemPos = new Vector3(Random.Range(-10.5f, 10.5f), -30, 0); // 위치 지정
        jB = Instantiate(itemBlocks[Random.Range(0, itemBlocks.Length)], ItemPos, Quaternion.identity); // 랜덤이고 중복아닌 리스트 값을 스폰할 인덱스에 넣고 스폰
        jB.transform.SetParent(gridObj.transform); // 그리드 내부에 들어가게 함
        yield return new WaitForSeconds(4);
        StartCoroutine(ItemSpawnLoop());
    }
    /*  
     *      중복방지하며 스폰
     *  
    void Pickup(GameObject jam)
    {
        
        foreach(int num in list) // 리스트에 있는 모든 수
        {
            if(jamBlocks[num].name == jam.name) // 값과 인덱스가 일치하면
            {
                list.Remove(num); // 리스트에서 삭제
            }
        }
    
    
    }    

    IEnumerator CountTime()
    {
       RESTART:

            int c = Random.Range(0, list.Count); // 리스트 중에 랜덤으로 고름

        if(c == bf && list.Count != 1) // 전에 나왔고, 젬이 한 개 이상일 경우
        {
            goto RESTART; // 다시 랜덤돌림
        }
        else
        {
            bf = c; // 다음에 나올 젬과 중복 감지하게 함

        }
            Vector3 JamPos = new Vector3(Random.Range(-10.5f, 10.5f), -10, 0); // 위치 지정
            jB = Instantiate(jamBlocks[list[c]], JamPos, Quaternion.identity); // 랜덤이고 중복아닌 리스트 값을 스폰할 인덱스에 넣고 스폰
            jB.transform.SetParent(gridObj.transform); // 그리드 내부에 들어가게 함
            yield return new WaitForSeconds(3);

            StartCoroutine(CountTime()); // 반복 실행(리스트가 없어질 때까지)
    }
    */
}
