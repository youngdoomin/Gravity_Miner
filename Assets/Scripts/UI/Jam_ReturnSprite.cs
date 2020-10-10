using UnityEngine;

public class Jam_ReturnSprite : MonoBehaviour
{
    public Sprite UI_img;
    public int jamID;

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
        GameManager.Instance.getJam = true;
        var jamSpawn = GameObject.FindGameObjectWithTag("Boundary");
        jamSpawn.SendMessage("Pickup", jamID);

        //jamSpawn.SendMessage("Pickup", this);
        if (GameManager.Instance.repeat == false)
        {
            GameManager.Instance.jamCt++;
        }
        GameManager.Instance.cJamUI = UI_img;
        JamUI.spriteToUI(GameManager.Instance.jamCt);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //Destroy(gameObject);

    }
}
