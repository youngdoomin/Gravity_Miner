using UnityEngine;

public class ParticleRotate : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && Playercontroller.energy >= 0 || SubGravity.sp == SubGravity.speedLock)
        {
            //transform.Rotate(0, 0, -90);
            transform.Rotate(Vector3.left * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) && Playercontroller.energy >= 0 || SubGravity.sp == SubGravity.speedLock)
        {
            //transform.Rotate(0, 0, 90);
            transform.Rotate(Vector3.right * Time.deltaTime);

        }
        /*
        else
        {
            //transform.Rotate(0, 0, 0);
            
        }
        */
    }
}
