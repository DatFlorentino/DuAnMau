using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< HEAD
        if (other.CompareTag("Enemy"))
=======
        if (other.CompareTag("Trap"))
>>>>>>> Scene1-Dat
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
