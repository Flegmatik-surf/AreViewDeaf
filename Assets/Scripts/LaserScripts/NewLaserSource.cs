using System.Collections;
using UnityEngine;
using System;

public class NewLaserSource : MonoBehaviour
{
    bool start = true;
    bool laserActivated = false;
    public static event Action SignalLaser;
    [SerializeField] private float time_of_activation = 5f;
    [SerializeField] private float time_of_desactivation = 2f;


    void Start()
    {
        //StartCoroutine(Wait());
    }
    // Update is called once per frame
    void Update()
    {
        print(laserActivated);
        if (start == true)
        {
            StartCoroutine(LaserSound());
        }
    }

    IEnumerator LaserSound()
    {
        start = false;
        SoundManager.PlaySound(SoundManager.Sound.Laser_Load);
        laserActivated = true;
        yield return new WaitForSeconds(time_of_activation);
        SoundManager.PlaySound(SoundManager.Sound.Laser);
        laserActivated = false;
        yield return new WaitForSeconds(time_of_desactivation);
        start = true;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (laserActivated)
            {
                SignalLaser?.Invoke();
            }
        }
    }

}
