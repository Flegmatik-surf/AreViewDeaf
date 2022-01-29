using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float Direction = 1f;
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
    }


    // Update is called once per frame
    void Update()
    {

        speed = mapManager.GetTileWalkingSpeed(transform.position);

        //anim.SetFloat("Direction", 1f);

        anim.SetFloat("Speed", 0f);

        float h_input = 0;
        float v_input = 0;
        //anim.SetBool("Up", false);
        //anim.SetBool("Down", false);
        //anim.SetBool("Left", false);
        //anim.SetBool("Right", false);


        if (Input.GetKey(KeyCode.Z))
        {
            print("grooooooooooooooooo");
            //anim.SetBool("Up", true);
            Direction = 0f;
            anim.SetFloat("Speed", 1f);
            v_input += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //anim.SetBool("Down", true);
            Direction = 1f;
            anim.SetFloat("Speed", 1f);
            v_input -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //anim.SetBool("Right", true);
            Direction = 3f;
            anim.SetFloat("Speed", 1f);
            h_input += 1;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            //anim.SetBool("Left", true);
            Direction = 2f;
            anim.SetFloat("Speed", 1f);
            h_input -= 1;
        }

        anim.SetFloat("Direction", Direction);

        rb.velocity = new Vector2(h_input, v_input).normalized * speed;
    }
}
