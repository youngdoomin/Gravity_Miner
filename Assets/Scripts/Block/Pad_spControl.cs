using UnityEngine;

public class Pad_spControl : MonoBehaviour
{
    enum PadType
    {
        Up,
        Down
    }
    [SerializeField]
    PadType Type;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player" && SubGravity.reaction == false)
        {
            if (Type == PadType.Up)
            {
                SubGravity.sp = SubGravity.speedLock;
            }
            else if (Type == PadType.Down)
            {
                SubGravity.sp = 0;
            }

        }

    }
}

