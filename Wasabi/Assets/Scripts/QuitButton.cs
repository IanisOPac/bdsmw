using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void quit()
    {
        Debug.Log("Jeu Fermé");
        Application.Quit();
    }
}


