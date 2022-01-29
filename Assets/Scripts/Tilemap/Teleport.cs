using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject arrival;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "Player")
        {
            collision.transform.position = arrival.transform.position;
        }
    }
}
