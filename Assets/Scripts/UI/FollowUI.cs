using UnityEngine;

public class FollowUI : MonoBehaviour
{
    public GameObject follow;

    // Update is called once per frame
    void Update()
    {
        Vector2 UIPose = Camera.main.WorldToScreenPoint(this.transform.position);
        follow.gameObject.transform.position = UIPose;
    }
}
