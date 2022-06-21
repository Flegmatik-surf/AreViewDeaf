using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSound : MonoBehaviour
{

    private AudioSource hitwall;

    private void Start()
    {
        hitwall = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitwall.Play();
    }
}
