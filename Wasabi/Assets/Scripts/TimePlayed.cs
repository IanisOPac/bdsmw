
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePlayed : MonoBehaviour
{
    Text txt;
    Vector3 character_position;
    Vector3 mouse_position;
    float mouse_distance;
    Vector3 souris;
    Vector2 unitVect;

    // Use this for initialization
    void Start()
    {
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "Time : " + System.Math.Round(Time.time, 1);

    }
}
