using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rig;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpspeed = 5f;
    CapsuleCollider2D col;
    float startgravityscale;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {

        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        startgravityscale = rig.gravityScale;
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();
    }

    void Run()
    {
        rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);
        bool havemove = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", havemove);
    }

    void Flip()
    {
        bool havemove = Mathf.Abs(moveInput.x) > Mathf.Epsilon;

        if (havemove)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), 1f);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);

    }

    void OnJump(InputValue value)
    {
        var isTouchingGround = col.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (!isTouchingGround) return;
        if (value.isPressed)
        {
            rig.velocity += new Vector2(0f, jumpspeed);
        }
    }


}