using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusicVol : MonoBehaviour
{

    public Slider Volume;
    public AudioSource Music;

    void Update()
    {
        Music.volume = (Volume.value / 100);
        PlayerPrefs.SetFloat("volume", Music.volume);
    }
}
