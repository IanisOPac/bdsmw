using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour {
    public void Quitter()
    {
        Application.Quit();
        Debug.Log("Fermeture");
    }
}
