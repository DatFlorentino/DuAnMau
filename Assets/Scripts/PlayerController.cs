using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;  // Thêm thư viện này để sử dụng UI
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
<<<<<<< HEAD
    


    // Start is called before the first frame update
=======

    // PlayerSwimming
    public float swimSpeed = 5f;
    public float swimDrag = 1f;
    public float normalDrag = 5f;
    private bool isInWater = false;

    // Oxygen variables
    public Slider oxygenBar;
    public float maxOxygen = 10f;
    private float currentOxygen;

>>>>>>> Scene1-Dat
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        startgravityscale = rig.gravityScale;

        anim = GetComponent<Animator>();
        gravityScaleAtStart = rig.gravityScale;
        isAlive = true;
<<<<<<< HEAD
=======

        // Initialize oxygen
        currentOxygen = maxOxygen;
        oxygenBar.maxValue = maxOxygen;
        oxygenBar.value = currentOxygen;

        Debug.Log("Game started. isAlive: " + isAlive + ", currentOxygen: " + currentOxygen);
>>>>>>> Scene1-Dat
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
<<<<<<< HEAD
        Die();
=======

        if (isInWater)
        {
            Swim();
            HandleOxygen();
        }
        else
        {
            currentOxygen = maxOxygen;
            oxygenBar.value = currentOxygen;
        }
        HandleOxygen();
>>>>>>> Scene1-Dat
    }

    void Run()
    {
<<<<<<< HEAD
        rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);
        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", havemove);
=======
        if (!isInWater) // Chỉ chạy khi không ở trong nước
        {
            rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);
            bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
            anim.SetBool("isRunning", havemove);
        }
>>>>>>> Scene1-Dat
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

    private void HandleOxygen()
    {
        if (isInWater) // Kiểm tra nếu đang ở trong nước
        {
            currentOxygen -= Time.deltaTime;
            oxygenBar.value = currentOxygen;

            Debug.Log("Oxygen level: " + currentOxygen);

            if (currentOxygen <= 0)
            {
                Die();
            }
        }
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            isInWater = true;
            rig.gravityScale = 0f; // Vô hiệu hóa trọng lực khi bơi
            rig.drag = swimDrag;
            anim.SetBool("isSwimming", true);

            Debug.Log("Entered water. currentOxygen: " + currentOxygen);
        }
        else if (collision.gameObject.CompareTag("Trap") || collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            Debug.Log("Hit trap. Collision with: " + collision.gameObject.name);
            Die();
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

            // Reset oxygen when exiting water
            currentOxygen = maxOxygen;
            oxygenBar.value = currentOxygen;

            Debug.Log("Exited water. currentOxygen: " + currentOxygen);
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
<<<<<<< HEAD
        {
            return;
        }
        var isTouchingGround = rig.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (!isTouchingGround) return;
        if (value.isPressed)
=======
>>>>>>> Scene1-Dat
        {
            return;
        }
        if (isInWater) // Nhảy trong nước
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpspeed);
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
<<<<<<< HEAD
            Debug.Log(">>>>> Fire");
            //tao ra vien dan tai vi tri sung
            var oneBullet = Instantiate(bullet, gun.position, transform.rotation);
            //cung cap velocity cho vien dan tuy theo huong cua nhan vat
            if (transform.localScale.x < 0)
            {
                oneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-15, 0);
               
=======
            var oneBullet = Instantiate(bullet, gun.position, transform.rotation);
            if (transform.localScale.x < 0)
            {
                oneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-15, 0);
>>>>>>> Scene1-Dat
            }
            else
            {
                oneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(15, 0);
            }
<<<<<<< HEAD
            // huy vien dan sau 2 giay
            Destroy(oneBullet, 2);
        }
    }
    // leo thang
=======
            Destroy(oneBullet, 2);
        }
    }

>>>>>>> Scene1-Dat
    void ClimbLadder()
    {
        var isTouchingLadder = col.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        if (!isTouchingLadder)
        {
            rig.gravityScale = gravityScaleAtStart;
            anim.SetBool("isClimbing", false);
            return;
        }
<<<<<<< HEAD
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
        var isTouchingEnemy = rig.IsTouchingLayers(LayerMask.GetMask("Enemy", "Trap"));
        if (isTouchingEnemy)
        {
            isAlive = false;
            anim.SetTrigger("Dying");
            rig.velocity = new Vector2(0, 0);
            //Xu ly die
            FindObjectOfType<GameController>().ProcessPlayerDeath();
        }
=======
        var ClimbVelocity = new Vector2(rig.velocity.x, moveInput.y * climbSpeed);
        rig.velocity = ClimbVelocity;
        var playerHasVerticalSpeed = Mathf.Abs(rig.velocity.y) > Mathf.Epsilon;
        anim.SetBool("isClimbing", playerHasVerticalSpeed);
        rig.gravityScale = 0;
    }

    void Die()
    {
        if (!isAlive)
        {
            return;
        }

        isAlive = false;
        anim.SetTrigger("Dying");
        rig.velocity = Vector2.zero;
        FindObjectOfType<GameController>().ProcessPlayerDeath();

        Debug.Log("Player died.");
>>>>>>> Scene1-Dat
    }
}
