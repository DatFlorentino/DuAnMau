using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public float Jump = 5f;
    Rigidbody2D rig;
    BoxCollider2D col;
    Vector2 moveInput;
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);


    }
    void OnJump(InputValue value)
    {
        if (!col.IsTouchingLayers(LayerMask.GetMask("ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            rig.velocity += new Vector2(0f, Jump);
        }
    }
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();

    }
    void Run()
    {   //rig.velocity = moveInput Game 4 huong
        rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);
    }
    void Flip()
    {
        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;

        if (havemove)
            transform.localScale = new Vector2(Mathf.Sign(rig.velocity.x), 1f);
    }
}