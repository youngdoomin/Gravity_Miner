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

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            if (Type == Item.Heart && Playercontroller.maxLife != GameManager.Instance.life)
            {
                GameManager.Instance.life++;
                HPManager.Heal(GameManager.Instance.life);
            }
            else if (Type == Item.Mana)
            { GameManager.Instance.energy = PGravity.fenergy;}
            else if (Type == Item.Shield)
            { GameManager.Instance.shieldOn = true;}
            this.gameObject.SetActive(false);
        }
        
    }
}
