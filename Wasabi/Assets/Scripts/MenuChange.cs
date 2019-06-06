using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChange : MonoBehaviour
{

    public void ChangesceneMenu(string scenename)
    {
        Application.LoadLevel(scenename);
    }
}
