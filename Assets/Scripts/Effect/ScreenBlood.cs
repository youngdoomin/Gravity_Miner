using UnityEngine;

public class ScreenBlood : MonoBehaviour
{
    public MeshRenderer bloody;
    private float transparency = 0;
    public float transTime = 1.0f;

    void Start()
    {
        bloody = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        bloody.material.color = new Color(1, 1, 1, Random.Range(transparency - 0.4f, transparency));

        if (Playercontroller.untouchable == true && transparency < 0.7)
        { transparency += 0.1f; }
        else if (Playercontroller.untouchable == false && transparency > 0)
        { transparency -= 0.1f; }

    }

}
