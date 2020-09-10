using UnityEngine;

public class Block_special : MonoBehaviour
{
    enum Item
    {
        Heart,
        Mana,
        Shield
    }
    [SerializeField]
    Item Type;

    public static bool shieldOn;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            if (Type == Item.Heart && Playercontroller.maxLife != Playercontroller.life)
            {
                Playercontroller.life++;
                HPManager.Heal(Playercontroller.life);
            }
            else if (Type == Item.Mana)
            {Playercontroller.energy = PGravity.fenergy;}
            else if (Type == Item.Shield)
            {shieldOn = true;}
            this.gameObject.SetActive(false);
        }
        
    }
}
