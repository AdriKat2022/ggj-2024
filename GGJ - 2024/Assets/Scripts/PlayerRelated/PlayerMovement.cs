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

    private Rigidbody2D rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = new(0, 0);
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 velocity = Vector2.zero;

        velocity.x = playerInput.horizontalAxis * speed;
        velocity.y = playerInput.verticalAxis * speed;

        rb.velocity = velocity;
    }
}
