using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{

    public int numberOfHearth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int health;

    private void Start()
    {
        numberOfHearth = hearts.Length;
        health = HPsaver.Instance.HP;

    }
    void Update()
    {
        if (health > numberOfHearth)
        {
            health = numberOfHearth;
        }
        for (int i = 0; i < numberOfHearth; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numberOfHearth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
