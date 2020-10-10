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
        if (coll.gameObject.name == "Player")
        {
            if (Type == PadType.Up)
            {GameManager.Instance.sp = GameManager.Instance.speedLock;}
            else if (Type == PadType.Down)
            { GameManager.Instance.sp = 0;}
        }
    }
}

