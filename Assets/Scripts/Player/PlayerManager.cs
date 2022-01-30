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
    int damage = 0;

    [SerializeField]private Fader fader;


    private void Awake()
    {

        mapManager = FindObjectOfType<MapManager>();
        MapManager.SignalSandDamage += OnSignalTakeDamage;
        Spike.SignalSpike += OnSignalTakeDamage;
        LaserSource.SignalLaser += OnSignalTakeDamage;
        Patrol.SignalPatrol += OnSignalTakeDamage;
        //Fader fader = FindObjectOfType<Fader>();
    }
    void Start()
    {
        currentHealth = healthHeart.numberOfHearth;
    }

    // Update is called once per frame
    void Update()
    {
        try{ mapManager.Effect(transform.position); }
        catch { }

        

    }

    void TakeDamage(int damage)
    {
        if (invincibilty == false)
        {
            StartCoroutine(Invicible(3));
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

    IEnumerator Invicible(int cooldown)
    {
        invincibilty = true;
        yield return new WaitForSeconds(cooldown);
        invincibilty = false;
        yield return null;
    }

    private void OnSignalTakeDamage()
    {
        print("ssssssssssssssssssssssssssssssss");
        TakeDamage(1);
    }

    private void OnDestroy()
    {
        MapManager.SignalSandDamage -= OnSignalTakeDamage;
        Spike.SignalSpike -= OnSignalTakeDamage;
        LaserSource.SignalLaser -= OnSignalTakeDamage;
        Patrol.SignalPatrol -= OnSignalTakeDamage;
    }


    private IEnumerator FadeIn(float time)
    {
        Fader fader = FindObjectOfType<Fader>();
       yield return fader.FadeIn(time);

    }
    private IEnumerator FadeOut(float time)
    {
        Fader fader = FindObjectOfType<Fader>();
        yield return fader.FadeOut(time);

    }
}
