using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValeurVol : MonoBehaviour {

    Text Valeur;

    private void Start()
    {
        Valeur = GetComponent<Text>();
    }

    public void TextUpdate (float value)
    {
        Valeur.text = Mathf.RoundToInt(value * 100) + " ";
        PlayerPrefs.SetFloat("volume", value);
    }
}

