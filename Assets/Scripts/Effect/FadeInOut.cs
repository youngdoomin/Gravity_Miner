using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    private bool once = false;
    public Animator animator;
    
    void Update()
    {
        if(Playercontroller.life == 0 && once == false)
        {
            animator.enabled = true;
            animator.SetBool("FadeOutT", true);
            once = true;
        }
    }
}