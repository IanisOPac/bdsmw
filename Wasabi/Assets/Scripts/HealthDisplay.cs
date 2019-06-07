using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {
    private int health;
    private int healthMax;
    private int equipe;
    private int numWorms;
    private float time;
    [SerializeField] public Text healthText;
    [SerializeField] public Text infoEnnemy;
    [SerializeField] public Text indication;
    private int hpMaxSoldiers;
    [SerializeField] Image jHealth1;
    [SerializeField] Image jHealth2;
    private int[] AllHp = new int[]{0,0};
    private void Start()
    {
        healthText.enabled = false;
        infoEnnemy.enabled = false;
    }

    private void Update()
    {
        healthText.text = "Soldat n°" + numWorms + 
                          "\nVie : " + health + "/" + healthMax +
                          "\nTimer : " + time;
        
    }
    public void SetEnnemyInfo(int currentNumEquipe, int currentHealth, int currentHealthMax, int currentNumWorms)
    {
        infoEnnemy.text = "Equipe n°" + currentNumEquipe +
       "\nSoldat n°" + currentNumWorms +
       "\nVie : " + currentHealth + "/" + currentHealthMax;
    }

    public void CleanEnnemyInfo()
    {
        infoEnnemy.text = "";
    }
    public void SetHp()
    {
        hpMaxSoldiers = GameObject.Find("universe_laws").GetComponent<Universe>().NumWormsMax * GameObject.FindGameObjectsWithTag("characters")[0].GetComponent<Char_script>().HpMax;
        AllHp = new int[] { 0, 0 };
        foreach (GameObject character in GameObject.FindGameObjectsWithTag("characters"))
        {
            if (character.GetComponent<Char_script>().NumEquipe == 1)
            {
                AllHp[0] = AllHp[0] + character.GetComponent<Char_script>().Hp;
                AllHp[0] = Mathf.Clamp(AllHp[0], 0, hpMaxSoldiers);
                float amount = (float)AllHp[0] / hpMaxSoldiers;
                jHealth1.fillAmount = amount;

            }
            else
            {
                AllHp[1] = AllHp[1] + character.GetComponent<Char_script>().Hp;
                AllHp[1] = Mathf.Clamp(AllHp[1], 0, hpMaxSoldiers);
                float amount = (float)AllHp[1] / hpMaxSoldiers;
                jHealth2.fillAmount = amount;
            }
        }
        if (jHealth1.fillAmount == 0)
        {
            indication.text = "Le joueur 2 a gagné !\nAppuyez sur echap";
            indication.enabled = true;
            GameObject.Find("universe_laws").GetComponent<Universe>().fini = true;
        }
        else if (jHealth2.fillAmount == 0)
        {
            indication.text = "Le joueur 1 a gagné !\nAppuyez sur echap";
            indication.enabled = true;
            GameObject.Find("universe_laws").GetComponent<Universe>().fini = true;
        }
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
        time = 15 - currentTime;
        time = Mathf.Round(time);        
    }
}
