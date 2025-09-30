using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public InputActionReference move;
    
    private Rigidbody2D rb;
    private bool moving;
    private Animator anim;
    private SpriteRenderer sr;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        moving = rb.linearVelocityX > .1 || rb.linearVelocityX < -.1 ? true : false;
        anim.SetBool("moving", moving);
        if (rb.linearVelocityX < 0) sr.flipX = true;
        if (rb.linearVelocityX > 0) sr.flipX = false;
        
    }

    private void FixedUpdate()
    {
        if (DialogueManager.instance.dialogueIsPlaying) return; 
        WASDMove();
    }

    public void WASDMove()
    {
        Vector2 moveDir = move.action.ReadValue<Vector2>();
        rb.linearVelocityX = moveDir.x * moveSpeed;
    }
}
