using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Animator anim;
    private MapManager mapManager;

    // Start is called before the first frame update//
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    private void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
        print(mapManager);
    }


    // Update is called once per frame
    void Update()
    {

        speed = mapManager.GetTileWalkingSpeed(transform.position);
        print(speed);





        float h_input = 0;
        float v_input = 0;
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);


        if (Input.GetKey(KeyCode.Z))
        {
            print("grooooooooooooooooo");
            anim.SetBool("Up", true);
            v_input += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Down", true);
            v_input -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Right", true);
            h_input += 1;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetBool("Left", true);
            h_input -= 1;
        }

        rb.velocity = new Vector2(h_input, v_input).normalized * speed;
    }
}
