using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolSlider : MonoBehaviour
{

    public Text percentageText;

    void Start()
    {
        percentageText = GetComponent<Text>();
    }

    public void TextUpdate(float value)
    {
        percentageText.text = Mathf.RoundToInt(value) + " ";
    }
}
