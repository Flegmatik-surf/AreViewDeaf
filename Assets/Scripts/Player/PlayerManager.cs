using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{ 
    public HealthHeart healthHeart;
    int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = healthHeart.numberOfHearth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        if (currentHealth - damage < 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }
        
        healthHeart.health = currentHealth;
    }
}
