using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateScript : MonoBehaviour
{
    [SerializeField] private Sprite PlateUp;
    [SerializeField] private Sprite PlateDown;
    SpriteRenderer spriteRenderer;
    [SerializeField] int indexOfPlate;
    private bool activated = false;
    

    public static event Action<int> ThePlateActivate;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        ControllerScript.ResetSignal += MakePlateUp;
    }
    // Update is called once per frame
    void Update()
    {
       
        
    }
    void MakePlateDown()
    {
        spriteRenderer.sprite = PlateDown;
        activated = true;
        //ajouter son de plaque appuyée
    }
    void MakePlateUp()
    {
        spriteRenderer.sprite = PlateUp;
        activated = false;
        //ajouter son de plaque relevée
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && !activated)
        {
            this.MakePlateDown();
            ThePlateActivate?.Invoke(indexOfPlate);
        }
    }

    private void OnDestroy()
    {
        ControllerScript.ResetSignal -= MakePlateUp;
    }

    
}
