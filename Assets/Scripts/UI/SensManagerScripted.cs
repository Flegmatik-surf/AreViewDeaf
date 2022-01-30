using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensManagerScripted : MonoBehaviour
{
    [SerializeField] private Fader fader;
    [SerializeField] bool see = true;
    [SerializeField] float time = 10f;

    // Start is called before the first frame update

    void Start()
    {
        if (see)
        {
            fader.FadeInGlobal(1f);
        }
        else
        {
            fader.FadeOutGlobal(1f);
        }
    }

    IEnumerator BlindToSee(float time)
    {
        fader.FadeInGlobal(1f);
        yield return null;
        fader.FadeOutGlobal(1f);
    }

    IEnumerator SeeToBlind(float time)
    {
        fader.FadeOutGlobal(1f);
        yield return null;
        fader.FadeInGlobal(1f);
    }
}

