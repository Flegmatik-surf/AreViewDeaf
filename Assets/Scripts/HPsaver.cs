using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPsaver : MonoBehaviour
{
    public static HPsaver Instance;
    public int HP;
    public int maxHP;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        HPsaver.Instance.HP = HPsaver.Instance.maxHP;
    }
}
