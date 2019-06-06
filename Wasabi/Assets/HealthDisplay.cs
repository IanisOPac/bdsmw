using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {
    private int health;
    private int healthMax;
    private int equipe;
    private int numWorms;
    private float time;
    public Text healthText;

    private void Update()
    {
        healthText.text = "Equipe n°" + equipe +
            "\nWorms n°" + numWorms +
            "\nVie : " + health + "/" + healthMax +
            "\nTimer : " + time;
        
    }

    public void SetText(int currentEquipe, int currentNumWorms ,int currentHealth, int currentHealthMax)
    {
        health = currentHealth;
        healthMax = currentHealthMax;
        equipe = currentEquipe;
        numWorms = currentNumWorms;
    }

    public void SetTime(float currentTime)
    {
        time = 10 - currentTime;
        time = Mathf.Round(time);
    }
}
