using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValeurVol : MonoBehaviour {

    Text Valeur;

    private void Start()
    {
        Valeur = GetComponent<Text>();
        PlayerPrefs.SetInt("bomb_damage", Mathf.RoundToInt(GameObject.Find("Slider_bomb").GetComponent<Slider>().value*100));

    }

    public void TextUpdateSound(float value)
    {
        Valeur.text = Mathf.RoundToInt(value * 100) + " ";
        PlayerPrefs.SetFloat("volume", value);
    }

    public void TextUpdateBombDamage(float value)
    {
        Valeur.text = Mathf.RoundToInt(value * 100) + " ";
        PlayerPrefs.SetInt("bomb_damage", Mathf.RoundToInt(value *100));
    }
}

