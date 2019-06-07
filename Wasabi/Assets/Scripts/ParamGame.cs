using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParamGame : MonoBehaviour {

    void Start()
    {
        PlayerPrefs.SetFloat("timer", 10);
        Sec10(".");
        Sec15("");
        Sec20("");
    }
    public void Recieve(string function)
    {
        
        switch (function)
        {
            case "10 secondes": PlayerPrefs.SetFloat("timer", 10);
                                Sec10(".");
                                Sec15("");
                                Sec20(""); break;

            case "15 secondes": PlayerPrefs.SetFloat("timer", 15);
                                Sec15(".");
                                Sec10("");
                                Sec20(""); break;

            case "20 secondes": PlayerPrefs.SetFloat("timer", 20);
                                Sec20(".");
                                Sec10("");
                                Sec15(""); break;
        }
    }

    private void Sec10(string val)
    {
        GameObject.Find("select10s").GetComponent<Text>().text = val; 
    }

    private void Sec15(string val)
    {
        GameObject.Find("select15s").GetComponent<Text>().text = val;
    }

    private void Sec20(string val)
    {
        GameObject.Find("select20s").GetComponent<Text>().text = val;
    }
}
