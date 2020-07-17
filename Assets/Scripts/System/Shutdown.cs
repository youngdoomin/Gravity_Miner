using UnityEngine;

public class Shutdown : MonoBehaviour
{
    public void Shut()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}
