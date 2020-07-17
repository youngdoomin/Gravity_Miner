using UnityEngine;

public class Item_random : MonoBehaviour
{
    public GameObject[] Items;
    private GameObject parentGrid;
    private GameObject randomItemSp;
    // Start is called before the first frame update
    void Start()
    {
        parentGrid = GameObject.Find("Grid");
        ItemSpawn();
    }
    /*
    void Update()
    {
        int jamCt;
        for (jamCt = 0; jamCt < Blocks.Length; jamCt++)
        {

        }
        

    }
    */
    void ItemSpawn()
    {
        randomItemSp = Instantiate(Items[UnityEngine.Random.Range(0, Items.Length)], transform.position, Quaternion.identity);
        randomItemSp.transform.SetParent(parentGrid.transform);
    }
}
