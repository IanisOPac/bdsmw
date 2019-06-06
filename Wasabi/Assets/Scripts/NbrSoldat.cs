using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NbrSoldat : MonoBehaviour {

    Text Text;
    int nbr = 1;
    string soldat = " soldat";
    private void Start()
    {
        Text = GameObject.FindGameObjectWithTag("text").GetComponent<Text>();
        Text.text = nbr.ToString() + soldat;
    }
     public void OnclickPlus ()
    {
        nbr++;
        if (nbr > 1)
        {
            soldat = " soldats";
        }
        if (nbr > 5)
        {
            nbr = 5;
        }
        Text.text = nbr.ToString() + soldat;
	}

    public void OnclickMoins()
    {
        nbr--;
        if (nbr > 1)
        {
            soldat = " soldats";
        }
        else if (nbr < 1)
        {
            nbr = 1;
        }
        else
        {
            soldat = " soldat";
        }
        Text.text = nbr.ToString() + soldat;
    }
}
