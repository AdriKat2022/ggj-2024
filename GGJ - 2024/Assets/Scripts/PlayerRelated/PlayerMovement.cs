using UnityEngine;

public struct PlayerInputObj
{
    public float horizontalAxis;
    public float verticalAxis;
    public bool attack;

    public PlayerInputObj(float horizontalAxis, float verticalAxis, bool attack)
    {
        this.horizontalAxis = horizontalAxis;
        this.verticalAxis = verticalAxis;
        this.attack = attack;
    }
}

// Moves the player based on the player input
public class PlayerMovement : PlayerControllable
{

    [Header("Movement")]
    [SerializeField]
    private float speed;

    [Header("Attack")]
    [SerializeField]
    private int attackPower = 9999;
    [SerializeField]
    private float endlag = .8f;
    [SerializeField]
    private PlayerAttackModule attackModule;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    private Rigidbody2D rb;
    private bool isAttacking;
    private float coolDown;
    private float facingDirection;

    public bool canMove = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackModule.SetDamage(attackPower);

        playerInput = new(0, 0, false);
        isAttacking = false;
    }

    private void Update()
    {
        if (canMove) {
            facingDirection = GetFacingDirection();
            Attack();
            MovePlayer();
            Animate();
        }
    }

    private void MovePlayer()
    {
        if(isAttacking) {
            coolDown -= Time.deltaTime;

            if(coolDown < 0)
                isAttacking = false;

            rb.velocity = Vector2.zero;

            return;
        }

        Vector2 velocity = Vector2.zero;

        velocity.x = playerInput.horizontalAxis * speed;
        velocity.y = playerInput.verticalAxis * speed;

        

        rb.velocity = velocity;
    }

    private float GetFacingDirection()
    {
        if (playerInput.verticalAxis > 0.1)
            return 180;
        if (playerInput.horizontalAxis > 0.1)
            return 90;
        if (playerInput.horizontalAxis < -0.1)
            return 270;
        if (playerInput.verticalAxis < -0.1)
            return 0;
        else
        {
            return facingDirection;
        }
    }

    private void Attack()
    {
        if (!playerInput.attack || isAttacking)
        { return; }
        attackModule.gameObject.SetActive(true);
        coolDown = endlag;
        isAttacking = true;
        animator.SetTrigger("Attack");
        attackModule.UpdateRotation(facingDirection);
        attackModule.ActivateModule();
    }

    private void Animate()
    {
        if (rb.velocity != Vector2.zero)
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("moveX", rb.velocity.x);
            animator.SetFloat("moveY", rb.velocity.y);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
        
}
