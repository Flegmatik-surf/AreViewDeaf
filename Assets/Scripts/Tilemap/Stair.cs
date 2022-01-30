using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private int nextScene=0;
    private void OnTriggerEnter2D(Collider2D collision) { 
        if (collision.gameObject.tag == "Player")
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
        
    }
}
