using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSprite : MonoBehaviour
{
    [SerializeField] private float time_of_activation = 5f;
    [SerializeField] private float time_of_desactivation = 2f;
    //[SerializeField] private Sprite sprite;
    private bool CoroutineInProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimingLaser(time_of_activation, time_of_desactivation));
    }

    // Update is called once per frame
    void Update()
    {
        if (!CoroutineInProgress)
        {
            StartCoroutine(TimingLaser(time_of_activation, time_of_desactivation));
        }
    }
    IEnumerator TimingLaser(float time_of_activation, float time_of_desactivation)
    {
        CoroutineInProgress = true;
        ChangeStateSprite(true);
        yield return new WaitForSeconds(time_of_activation);
        ChangeStateSprite(false);
        yield return new WaitForSeconds(time_of_desactivation);
        CoroutineInProgress = false;

    }

    void ChangeStateSprite(bool state)
    {
        GetComponent<SpriteRenderer>().enabled = state;    }
}
