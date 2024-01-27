using UnityEngine;

public struct PlayerInputObj
{
    public float horizontalAxis;
    public float verticalAxis;

    public PlayerInputObj(float horizontalAxis, float verticalAxis)
    {
        this.horizontalAxis = horizontalAxis;
        this.verticalAxis = verticalAxis;
    }
}

// Moves the player based on the player input
public class PlayerMovement : PlayerControllable
{
    [Header("Movement")]
    [SerializeField]
    private float speed;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerInput = new(0, 0);
    }

    private void Update()
    {
        MovePlayer();
        Animate();
    }

    private void MovePlayer()
    {
        Vector2 velocity = Vector2.zero;

        velocity.x = playerInput.horizontalAxis * speed;
        velocity.y = playerInput.verticalAxis * speed;

        rb.velocity = velocity;
    }

    private void Animate()
    {
        animator.SetFloat("HorizontalInput", rb.velocity.x);
        animator.SetFloat("VerticalInput", rb.velocity.y);
    }
        
}
