using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWorms : MonoBehaviour
{
    private GameObject selected;
    GameObject clone;

    [SerializeField] GameObject charact;
    private void Start()
    {
        for (int i = 1; i <= 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                clone = Instantiate(charact, new Vector3(transform.position.x + i, transform.position.y), transform.rotation);
                clone.GetComponent<Char_script>().setNumEquipe(i);
            }
        }
        selected = GameObject.FindGameObjectsWithTag("characters")[0];
    }
    private void Update()
    {

    }

    public void SetSelected(GameObject select)
    {
        selected = select;
    }

    public Char_script GetSelected()
    {
        return selected.GetComponent<Char_script>();
    }
}
