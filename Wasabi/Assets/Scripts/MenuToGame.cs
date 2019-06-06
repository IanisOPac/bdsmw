using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToGame : MonoBehaviour
{

    public void Click(string scene)
    {
        SceneManager.LoadScene(scene);
        PlayerPrefs.SetInt("nbSoldiers", GameObject.FindGameObjectWithTag("text").GetComponent<NbrSoldat>().Nbr);
    }
}