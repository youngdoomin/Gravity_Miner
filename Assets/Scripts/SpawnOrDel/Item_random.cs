using UnityEngine;

public class Item_random : MonoBehaviour
{
    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        int randomIdx = Random.Range(0, transform.childCount - 1);
        transform.GetChild(randomIdx).gameObject.SetActive(true);

    }
    /*
    enum ItemType
    {
        Item,
        Jam
    }
    [SerializeField]
    ItemType Type;
    // Start is called before the first frame update
    void Start()
    {
        if (Type == ItemType.Item) { GameManager.Instance.CallItemRandom(this.gameObject.transform); }
        else { GameManager.Instance.CallJamRandom(this.gameObject.transform); }
    }
    */
}
