using UnityEngine;

public class Jam_Item_Return : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")// && JamUI.delay == false)
        {
            this.gameObject.SendMessage("JamReturn");
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")// && JamUI.delay == false)
        {
            this.gameObject.SendMessage("JamReturn");
        }
    }
}
