using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerManager : MonoBehaviour
{

    //health
    public HealthHeart healthHeart;
    int currentHealth;

    //map
    private MapManager mapManager;

    //degat
    public bool invincibilty = false;

    //indice de la scene à load si on meurt
    [SerializeField] private int deathScene;



    private void Awake()
    {

        mapManager = FindObjectOfType<MapManager>();
        MapManager.SignalSandDamage += OnSignalTakeDamage;
        Spike.SignalSpike += OnSignalTakeDamage;
        LaserSource.SignalLaser += OnSignalTakeMoreDamage;
        NewLaserSource.SignalLaser += OnSignalTakeMoreDamage;
        Patrol.SignalPatrol += OnSignalTakeDamage;
    }
    void Start()
    {
        //currentHealth = healthHeart.numberOfHearth;
        currentHealth= HPsaver.Instance.HP;
    }

    public void SavePlayer()
    {
        HPsaver.Instance.HP = currentHealth;
    }

    public void RecharchePlayer()
    {
        currentHealth = HPsaver.Instance.HP;

    }

    // Update is called once per frame
    void Update()
    {
        print(currentHealth);
        try{ mapManager.Effect(transform.position); }
        catch { }
        if (currentHealth <= 0)
        {
             UnityEngine.SceneManagement.SceneManager.LoadScene(deathScene);
        }

    }

    void TakeDamage(int damage)
    {
        if (invincibilty == false)
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
            SavePlayer();
            StartCoroutine(Invicible(1));
        }

    }
    IEnumerator Invicible(int cooldown)
    {
        invincibilty = true;
        yield return new WaitForSeconds(cooldown);
        invincibilty = false;
        yield return null;
    }

    private void OnSignalTakeDamage()
    {
        TakeDamage(1);
    }
    private void OnSignalTakeMoreDamage()
    {
        TakeDamage(2);
    }

    private void OnDestroy()
    {
        MapManager.SignalSandDamage -= OnSignalTakeDamage;
        Spike.SignalSpike -= OnSignalTakeDamage;
        LaserSource.SignalLaser -= OnSignalTakeMoreDamage;
        NewLaserSource.SignalLaser -= OnSignalTakeMoreDamage;
        Patrol.SignalPatrol -= OnSignalTakeDamage;
    }

}
