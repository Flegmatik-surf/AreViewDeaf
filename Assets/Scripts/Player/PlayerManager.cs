using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //health
    public HealthHeart healthHeart;
    int currentHealth;
    //map
    private MapManager mapManager;
    //degat
    bool invincibilty = false;




    private void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
    }
    void Start()
    {
        currentHealth = healthHeart.numberOfHearth;
    }

    // Update is called once per frame
    void Update()
    {
        int damage = mapManager.GetTileDamage(transform.position);
        mapManager.Effect(transform.position);
        if (invincibilty == false)
        {
            TakeDamage(damage);
// StartCoroutine(Invicible(3));
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

    IEnumerator Invicible(int cooldown)
    {
        invincibilty = true;
        yield return new WaitForSeconds(cooldown);
        invincibilty = false;
        yield return null;
    }
}
