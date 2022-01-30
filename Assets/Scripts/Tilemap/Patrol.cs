using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;
    public int speed;

    private int waypointIndex;
    private float dist;

    public char orientation;

    public static event Action SignalPatrol;

    void Start()
    {
        waypointIndex = 0;

    }

    void Update()
    {

        //transform.LookAt(waypoints[waypointIndex].position);
        dist = Vector2.Distance(transform.position, waypoints[waypointIndex].position);
        PatrolMovement();
       


    }

    void PatrolMovement()
    {
        Vector2 direction = (waypoints[waypointIndex].position - transform.position).normalized;


        //if (Math.Abs(direction.x) < Math.Abs(direction.y))
        //{
        //    if (direction.y > 0)
        //    {
        //        orientation = 'U';
        //    } else
        //    {
        //        orientation = 'D';
        //    }
        //} else {
        //    if (direction.x > 0)
        //    {
        //        orientation = 'R';
        //    } else
        //    {
        //        orientation = 'L';
        //    }
        //}
        transform.Translate(direction * speed * Time.deltaTime);
        if (dist < 0.1f)
        {
            IncreaseIndex();
            direction = (waypoints[waypointIndex].position - transform.position).normalized;
            float gauche_droite = direction.x;
            float haut_bas = direction.y;
            print(direction);

            if (gauche_droite > 0)
            {
                if (haut_bas > gauche_droite)
                {
                    orientation = 'U';
                }
                else if (haut_bas < 0 && -haut_bas > gauche_droite)
                {
                    orientation = 'D';
                }
                else { orientation = 'R'; }
            }
            else
            {
                if (haut_bas > -gauche_droite)
                {
                    orientation = 'U';
                }
                else if (haut_bas < 0 && -haut_bas > -gauche_droite)
                {
                    orientation = 'D';
                }
                else { orientation = 'L'; }
            }
            print(orientation);
        }
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SignalPatrol?.Invoke();
        }
    }

}
