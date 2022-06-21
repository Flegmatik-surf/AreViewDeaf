using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private int nextScene;
    private AudioSource source;

    private void Start()
    {
       source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision) { 
        if (collision.gameObject.tag == "Player")
        {
            source.Play();
            StartCoroutine(LoadScene());

        }
        
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
