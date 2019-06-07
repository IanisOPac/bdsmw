using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMove : MonoBehaviour
{
    public GameObject explosion;
    float timeExpl;
    Ferr2DT_PathTerrain fr;
    // Use this for initialization
    void Start()
    {
        //timeExpl = Time.time;
    }

    /*void Update()
    {
        if (Time.time - timeExpl > 3)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "characters")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
