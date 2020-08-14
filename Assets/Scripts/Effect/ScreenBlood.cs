using UnityEngine;

public class ScreenBlood : MonoBehaviour
{
    public MeshRenderer bloody;
    private float transparency = 0;
    public float transTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        bloody = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        bloody.material.color = new Color(1, 1, 1, Random.Range(transparency - 0.4f, transparency));
       
        
        if(Playercontroller.untouchable == true && transparency < 0.7)
        {
            transparency += 0.1f;
        }
        else if(Playercontroller.untouchable == false && transparency > 0)
        {
            transparency -= 0.1f;
        }
        if(PGravity.screenFilter == true)
        {

        }
      
    }

}
