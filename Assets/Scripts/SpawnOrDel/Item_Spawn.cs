using UnityEngine;

public class Item_Spawn : MonoBehaviour
{
    //public GameObject spawnPrefab;
    //private GameObject Jchild;
    // Start is called before the first frame update
    void Start()
    {
        //SpawnJam();
    }

    void SpawnJam()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        //Jchild = Instantiate(spawnPrefab, transform.position, Quaternion.identity); //게임옵젝생성(오브젝트, 위치, 회전)       identity 기본값
        //Jchild.transform.SetParent(this.gameObject.transform.parent);
    }
}
