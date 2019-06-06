using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{

    public GameObject TextDisplay;
    public Text TextItem;
    public string ItemType;
    public string ItemId;
    public string ItemDescription;
    public Sprite ItemSprite;
    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().sprite = ItemSprite;
        TextItem.text = ItemDescription;
        TextDisplay.SetActive(false);
    }
    public class Item
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowText()
    {
        TextDisplay.SetActive(true);
    }

    public void HideText()
    {
        TextDisplay.SetActive(false);
    }
}
