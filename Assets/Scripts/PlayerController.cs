using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rig;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpspeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    CapsuleCollider2D col;
    float startgravityscale;

    Animator anim;
    private float gravityScaleAtStart;
    private bool isAlive;


    // Start is called before the first frame update
    void Start()
    {

        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        startgravityscale = rig.gravityScale;

        anim = GetComponent<Animator>();
        gravityScaleAtStart = rig.gravityScale;
        isAlive = true;
    }
        // Update is called once per frame
        void Update()
    {
        Run();
        Flip();
        ClimbLadder();
        Die();
    }

    void Run()
    {
        rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);
        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", havemove);
    }

    void Flip()
    {
        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;

        if (havemove)
        {
            transform.localScale = new Vector2(Mathf.Sign(rig.velocity.x), 1f);
        }
    }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        var isTouchingGround = rig.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (!isTouchingGround) return;
        if (value.isPressed)
        {
            rig.velocity += new Vector2(0f, jumpspeed);
        }
    }
    // leo thang
    void ClimbLadder()
    {
        var isTouchingLadder = col.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        if (!isTouchingLadder)
        {
            rig.gravityScale = gravityScaleAtStart;
            anim.SetBool("isClimbing", false);
            return;
        }
        var ClimbVelocity = new Vector2 (rig.velocity.x, moveInput.y * climbSpeed);
        rig.velocity = ClimbVelocity;
        // dieu khien animation leo thang
        var playerHasVerticalSpeed = Mathf.Abs(rig.velocity.y) > Mathf.Epsilon;
        anim.SetBool("isClimbing", playerHasVerticalSpeed);
        // tat gravity khi leo thang
        rig.gravityScale = 0;
    }
    void Die()
    {
        var isTouchingEnemy = rig.IsTouchingLayers(LayerMask.GetMask("Enemy"));
        if (isTouchingEnemy)
        {
            isAlive = false;
            anim.SetTrigger("Dying");
            rig.velocity = new Vector2(0, 0);
        }
    }
}
