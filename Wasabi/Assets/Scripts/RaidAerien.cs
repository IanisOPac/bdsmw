using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidAerien : MonoBehaviour
{

    [SerializeField] GameObject pre_bombe_aerien;
    bool create = false;
    float timePassed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.A) && !create)
        {
            timePassed = Time.time;
            create = true;
            GameObject bomb;
            bomb = Instantiate(pre_bombe_aerien, new Vector2(transform.position.x + 3, transform.position.y), transform.rotation);
            float dist = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(mouse_pos.x, 0));
            //bomb.AddComponent<RaidAerienExplosion>();


            int hauteur = 100;
            float temps = Mathf.Sqrt(2 * hauteur / 9.8f);
            Vector2 vit = new Vector2(dist / temps, Mathf.Sqrt(2 * 9.8f * hauteur));
            Debug.Log(vit);
            bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(vit.x * 30, vit.y * 30));
        }

        if (Time.time - timePassed > 3)
        {
            create = false;
        }

    }
}
