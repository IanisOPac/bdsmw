using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameObject explosionPrefab;

    // Use this for initialization
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}