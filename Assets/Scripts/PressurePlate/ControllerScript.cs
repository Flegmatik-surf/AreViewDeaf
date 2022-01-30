using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerScript : MonoBehaviour
{
    private string sequence = "";
    private string solution = "01234";
    //lion 0, cri 1, cheval 2, serpent 3, chat 4
    // Start is called before the first frame update
    public static event Action ResetSignal;

    private void Awake()
    {
        PlateScript.ThePlateActivate += OnReceptionOfSignal;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (sequence.Length==5)
        {
            if (sequence == solution )
            {
                print("c'est gagné");
            }
            else
            {
                //signal pour faire reset les plates 
                sequence = "";
                ResetSignal?.Invoke();
                print("Signal de Reset envoyé");
            }
        }
    }

    private void OnReceptionOfSignal(int indexOfPlate)
    {
        sequence += indexOfPlate;
    }

    private void OnDestroy()
    {
        PlateScript.ThePlateActivate -= OnReceptionOfSignal;
    }
}
