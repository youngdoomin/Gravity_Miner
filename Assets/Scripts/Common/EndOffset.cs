using UnityEngine;

public class EndOffset : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SendMessage("EndOffset");
    }
}
