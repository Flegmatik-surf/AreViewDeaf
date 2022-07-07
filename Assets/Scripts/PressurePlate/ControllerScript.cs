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
    bool restart = true;
    [SerializeField] private int nextScene;
    //[SerializeField] GameObject teleport;

    private void Awake()
    {
        PlateScript.ThePlateActivate += OnReceptionOfSignal;
        //teleport.gameObject.GetComponent <Collider2D>().isTrigger = false;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (restart == true)
        {
            StartCoroutine(Enigme());
        }

        if (sequence.Length==5)
        {
            if (sequence == solution )
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
            }
            else
            {
                sequence = "";
                ResetSignal?.Invoke();
                print("Signal de Reset envoy?");
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

    IEnumerator Enigme()
    {
        restart = false;
        yield return new WaitForSeconds(2f);
        SoundManager.PlaySound(SoundManager.Sound.Lion);
        yield return new WaitForSeconds(2f);
        SoundManager.PlaySound(SoundManager.Sound.Human);
        yield return new WaitForSeconds(2f);
        SoundManager.PlaySound(SoundManager.Sound.Horse);
        yield return new WaitForSeconds(2f);
        SoundManager.PlaySound(SoundManager.Sound.Snake);
        yield return new WaitForSeconds(2f);
        SoundManager.PlaySound(SoundManager.Sound.Cat);
        yield return new WaitForSeconds(2f);


        restart = true;
        yield return null;
    }
}
