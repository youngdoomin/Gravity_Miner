using UnityEngine;
using System.Collections;

public class Item_Spawn : MonoBehaviour
{
    GameObject item;
    //public GameObject spawnPrefab;
    //private GameObject Jchild;
    // Start is called before the first frame update
    void Start()
    {
        item = gameObject.transform.GetChild(1).gameObject;
        //SpawnJam();
    }

    IEnumerator SpawnJam()
    {
        item.SetActive(true);
        item.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        item.GetComponent<BoxCollider2D>().enabled = true;

        //gameObject.transform.GetChild(1).gameObject.transform.localScale = new Vector3(1, 1, 0);
        //Jchild = Instantiate(spawnPrefab, transform.position, Quaternion.identity); //게임옵젝생성(오브젝트, 위치, 회전)       identity 기본값
        //Jchild.transform.SetParent(this.gameObject.transform.parent);
    }
}
