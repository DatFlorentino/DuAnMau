using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    private Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(moveSpeed,0);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed *= -1;
        // Xoay Huong cua quai vat
        transform.localScale = new Vector2(-(Mathf.Sign(rig.velocity.x)),1f);
    }
}
