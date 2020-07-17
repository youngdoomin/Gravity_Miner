using UnityEngine;

public class Pad_spControl : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player" && SubGravity.reaction == false)
        {
            if (this.gameObject.name == "SpUp_Pad")
            {
                SubGravity.sp = SubGravity.speedLock;
            }
            else if (this.gameObject.name == "SpDown_Pad")
            {
                SubGravity.sp = 0;
            }

        }

    }
}

