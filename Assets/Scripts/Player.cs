using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Import SceneManager

public class Player : MonoBehaviour
{
    public int health;
    public float speed;
    public Slider healthBar;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        moveVelocity = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        healthBar.value = health;

        // Kiểm tra nếu máu bằng 0
        if (health <= 0)
        {
            ReloadScene();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void ReloadScene()
    {
        // Tải lại màn hình hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
