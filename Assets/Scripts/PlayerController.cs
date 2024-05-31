using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    // PlayerSwimming
    public float swimSpeed = 5f;
    public float swimDrag = 1f;
    public float normalDrag = 5f;
    private bool isInWater = false;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        startgravityscale = rig.gravityScale;

        anim = GetComponent<Animator>();
        gravityScaleAtStart = rig.gravityScale;
        isAlive = true;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        Flip();
        ClimbLadder();
        Die();

        if (isInWater)
        {
            Swim();
        }
    }

    void Run()
    {
        if (!isInWater) // Chỉ chạy khi không ở trong nước
        {
            rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);
            bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
            anim.SetBool("isRunning", havemove);
        }
    }

    void Flip()
    {
        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;

        if (havemove)
        {
            transform.localScale = new Vector2(Mathf.Sign(rig.velocity.x), 1f);
        }
    }

    private void Swim()
    {
        // Sử dụng đầu vào của người chơi để di chuyển trong nước
        float horizontal = moveInput.x;
        float vertical = moveInput.y;

        Vector2 direction = new Vector2(horizontal, vertical);
        rig.velocity = direction * swimSpeed;
        rig.drag = swimDrag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            isInWater = true;
            rig.gravityScale = 0f; // Vô hiệu hóa trọng lực khi bơi
            rig.drag = swimDrag;
            anim.SetBool("isSwimming", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            isInWater = false;
            rig.gravityScale = gravityScaleAtStart; // Khôi phục trọng lực khi không bơi
            rig.drag = normalDrag;
            anim.SetBool("isSwimming", false);
        }
    }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        if (isInWater) // Nhảy trong nước
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpspeed = 18f);
        }
        else
        {
            var isTouchingGround = rig.IsTouchingLayers(LayerMask.GetMask("Ground"));
            if (!isTouchingGround) return;
            if (value.isPressed)
            {
                rig.velocity += new Vector2(0f, jumpspeed);
            }
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        if (value.isPressed)
        {
            var oneBullet = Instantiate(bullet, gun.position, transform.rotation);
            if (transform.localScale.x < 0)
            {
                oneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-15, 0);
            }
            else
            {
                oneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(15, 0);
            }
            Destroy(oneBullet, 2);
        }
    }

    void ClimbLadder()
    {
        var isTouchingLadder = col.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        if (!isTouchingLadder)
        {
            rig.gravityScale = gravityScaleAtStart;
            anim.SetBool("isClimbing", false);
            return;
        }
        var ClimbVelocity = new Vector2(rig.velocity.x, moveInput.y * climbSpeed);
        rig.velocity = ClimbVelocity;
        var playerHasVerticalSpeed = Mathf.Abs(rig.velocity.y) > Mathf.Epsilon;
        anim.SetBool("isClimbing", playerHasVerticalSpeed);
        rig.gravityScale = 0;
    }

    void Die()
    {
        var isTouchingEnemy = rig.IsTouchingLayers(LayerMask.GetMask("Enemy", "Trap"));
        if (isTouchingEnemy)
        {
            isAlive = false;
            anim.SetTrigger("Dying");
            rig.velocity = Vector2.zero;
            FindObjectOfType<GameController>().ProcessPlayerDeath();
        }
    }
}
