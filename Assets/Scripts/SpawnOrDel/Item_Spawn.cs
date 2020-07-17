using UnityEngine;

public class Item_Spawn : MonoBehaviour
{
    public GameObject spawnPrefab;
    private GameObject parentSp;
    private GameObject Jchild;
    // Start is called before the first frame update
    void Start()
    {
        parentSp = GameObject.Find("Grid");
        //SpawnJam();
    }

    void SpawnJam()
    {
        Jchild = Instantiate(spawnPrefab, transform.position, Quaternion.identity); //게임옵젝생성(오브젝트, 위치, 회전)       identity 기본값
        Jchild.transform.SetParent(parentSp.transform);
    }
}
