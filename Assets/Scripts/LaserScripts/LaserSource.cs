using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSource : MonoBehaviour
{
    [SerializeField] private Vector2 direction = Vector2.right;
    private LineRenderer lineRenderer;
    public Transform LaserHit;
    [SerializeField] private float time_of_activation = 5f;
    [SerializeField] private float time_of_desactivation = 2f;
    [SerializeField] private LayerMask layerMaskPlayer;

    [SerializeField] private float numberOfCase=1;
    private bool LaserActivated = false;
    private bool PreviousFrameStateLaser = false;
    private bool CoroutineInProgress = false;
    private bool invicibility;

    GameObject player;

    public static event Action SignalLaser;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        invicibility = player.GetComponent<PlayerManager>().invincibilty;
        player.GetComponent<Rigidbody2D>().WakeUp();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
        GameObject HitPoint = new GameObject();
        LaserHit = HitPoint.GetComponent<Transform>();
        StartCoroutine(TimingLaser(time_of_activation, time_of_desactivation));

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        Debug.DrawLine(transform.position, hit.point);
        LaserHit.position = hit.point;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, LaserHit.position);
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            lineRenderer.enabled = !(lineRenderer.enabled);
        }
        */
        //print("Laser Source : LaserActivated " + LaserActivated);
        //print("Laser Source : PreviousFrameStateLaser " + PreviousFrameStateLaser);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, numberOfCase*0.64f ,layerMaskPlayer);
        if ((LaserActivated) && (hitInfo))
        {
            if (invicibility == false)
            {
                SignalLaser?.Invoke();
            }
        }
        if ((LaserActivated) && (!PreviousFrameStateLaser))
        {
            print("son de d?but laser");
            SoundManager.PlaySound(SoundManager.Sound.Laser);
            PreviousFrameStateLaser = true; //change sauvegarde de l'?tat pr?c?dent
            //laser activ? alors que d?sactiver avant -> son de d?marrage laser
        }
        if ((!LaserActivated) && (PreviousFrameStateLaser))
        {
            print("son de fin laser");
            SoundManager.PlaySound(SoundManager.Sound.Laser_Load);
            PreviousFrameStateLaser = false; //change sauvegarde de l'?tat pr?c?dent
            //Laser d?sactiv? alors que activ? avant -> son d'?teingnage laser
        }
        if (!CoroutineInProgress)
        {
            StartCoroutine(TimingLaser(time_of_activation, time_of_desactivation));
        }
    }

    IEnumerator TimingLaser(float time_of_activation, float time_of_desactivation)
    {
        CoroutineInProgress = true;
        LaserActivated = true;
        yield return new WaitForSeconds(time_of_activation);
        LaserActivated = false;
        yield return new WaitForSeconds(time_of_desactivation);
        CoroutineInProgress = false;
    }
}
