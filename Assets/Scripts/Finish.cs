using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject informationCanvas;
    [SerializeField] GameObject finishCanvas;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            informationCanvas.SetActive(false);
            finishCanvas.SetActive(true);
        }
    }
}
