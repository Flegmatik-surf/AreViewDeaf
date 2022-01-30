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
    private bool LaserActivated = false;
    private bool PreviousFrameStateLaser = false;
    private bool CoroutineInProgress = false;



    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
        GameObject HitPoint = new GameObject();
        LaserHit = HitPoint.GetComponent<Transform>();
        //Instantiate(HitPoint);
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

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, 10f ,layerMaskPlayer);
        if ((LaserActivated) && (hitInfo))
        {
            print("grooo t moooor");
            //tuer joueur
        }
        if ((LaserActivated) && (!PreviousFrameStateLaser))
        {
            print("son de début laser");
            PreviousFrameStateLaser = true; //change sauvegarde de l'état précédent
            //laser activé alors que désactiver avant -> son de démarrage laser
        }
        if ((!LaserActivated) && (PreviousFrameStateLaser))
        {
            print("son de fin laser");
            PreviousFrameStateLaser = false; //change sauvegarde de l'état précédent
            //Laser désactivé alors que activé avant -> son d'éteingnage laser
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
        //SignalSandDamage?.Invoke();
        LaserActivated = false;
        yield return new WaitForSeconds(time_of_desactivation);
        CoroutineInProgress = false;
    }
}
