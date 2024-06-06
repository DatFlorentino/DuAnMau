using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public int health;
    public int damage;
    private float timeBtwDamage = 1.5f;

    public Animator camAnim;
    public Slider healthBar;
    private Animator anim;
    public bool isDead;

    public GameObject finishGatePrefab; // Tham chiếu đến Prefab của cổng hoàn thành

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 25)
        {
            anim.SetTrigger("stageTwo");
        }

        if (health <= 0 && !isDead)
        {
            anim.SetTrigger("death");
            Die();
        }

        // Cho người chơi một chút thời gian hồi phục trước khi nhận thêm sát thương
        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }

        healthBar.value = health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Gây sát thương cho người chơi
        if (other.CompareTag("Player") && isDead == false)
        {
            if (timeBtwDamage <= 0)
            {
                camAnim.SetTrigger("shake");
                other.GetComponent<Player>().health -= damage;
            }
        }
    }

    private void Die()
    {
        isDead = true;
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 2.0f, transform.position.z); // Giảm vị trí y xuống 2 đơn vị
        Instantiate(finishGatePrefab, spawnPosition, Quaternion.identity); // Hiện Prefab của cổng hoàn thành khi boss chết
    }
}
