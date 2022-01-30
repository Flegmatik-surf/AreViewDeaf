using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private int indexLevel;
    public static event Action<int> startSceneSignal;

    // Start is called before the first frame update
    void Start()
    {
        startSceneSignal?.Invoke(indexLevel);
    }

}
