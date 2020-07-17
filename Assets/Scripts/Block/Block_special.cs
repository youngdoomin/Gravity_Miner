using UnityEngine;

public class Block_special : MonoBehaviour
{
    public GameObject[] item;
    public static bool shieldOn;
    /*
    private void OnTriggerEnter2D(Collider2D coll)
    {


    }
    */
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            if (this.gameObject == item[0] && Playercontroller.maxLife != Playercontroller.life)
            {
                Playercontroller.life++;
                HPManager.Heal(Playercontroller.life);
            }
            else if (this.gameObject == item[1])
            {
                Playercontroller.energy = PGravity.fenergy;
            }
            else if (this.gameObject == item[2])
            {
                shieldOn = true;
            }
            Destroy(gameObject);
        }
        
    }
}
