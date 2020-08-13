using UnityEngine;

public class Item_random : MonoBehaviour
{
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
}
