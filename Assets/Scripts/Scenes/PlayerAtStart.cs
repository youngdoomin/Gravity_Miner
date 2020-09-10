using UnityEngine;

public class PlayerAtStart : MonoBehaviour
{
    private Animator animator;
    //private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public float speed = 5;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /*
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);

        if (rb.velocity.x > 0)
        {spriteRenderer.flipX = false;}
        else if (rb.velocity.x < 0)
        {spriteRenderer.flipX = true;}
        animator.SetFloat("MoveSpeed", Mathf.Abs(rb.velocity.x));
        */
        if (StartScreen.step == StartScreen._JUMP)
        {
            animator.SetTrigger("Drill");
        }
    }
}
