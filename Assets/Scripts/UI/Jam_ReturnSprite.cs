using UnityEngine;

public class Jam_ReturnSprite : MonoBehaviour
{
    public static int jamCt = 0;
    public static bool getJam;
    public Sprite UI_img;
    public int jamID;
    public static Sprite cJamUI;

    private void OnEnable()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        { JamReturn(); }

    }

    void JamReturn()
    {
        getJam = true;
        var jamSpawn = GameObject.FindGameObjectWithTag("Boundary");
        jamSpawn.SendMessage("Pickup", jamID);

        //jamSpawn.SendMessage("Pickup", this);
        if (JamUI.repeat == false)
        {
            jamCt++;
        }
        cJamUI = UI_img;
        JamUI.spriteToUI(jamCt);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //Destroy(gameObject);

    }
}
