using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        print("hop");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("aaaaaaaaaaaaaaaaaaa");
    }
}
