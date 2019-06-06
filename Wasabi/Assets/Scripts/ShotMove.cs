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
        timeExpl = Time.time;
    }

    void Update()
    {
        if (Time.time - timeExpl > 3)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
