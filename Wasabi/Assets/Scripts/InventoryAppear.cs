using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAppear : MonoBehaviour
{

    public GameObject Inventory;
    bool panelState;

    // Use this for initialization
    void Start()
    {
        Inventory.SetActive(false);
        panelState = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            panelState = !panelState;
            Inventory.SetActive(panelState);
        }

    }
}
