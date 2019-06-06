using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shot : MonoBehaviour
{
    GameObject cube, triangle;
    public GameObject prefabs_bullet;
    int strengh = 400;
    Mesh mesh_triangle;

    bool create = false;
    float timePassed, timeDuring;

    Vector3 character_position;
    Rigidbody rg;
    Vector3 mouse_position;
    float angle_souris;
    float mouse_distance;

    int dir;
    Vector2 g, f, unitVect, souris;
    // Use this for initialization
    void Start()
    {
        character_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        character_position = transform.position;

        if (Input.GetMouseButton(0) && !create)
        {
            mouse_position = Input.mousePosition;
            angle_souris = Mathf.Atan((mouse_position.y - character_position.y) / (mouse_position.x - character_position.x));
            mouse_distance = (mouse_position.y - character_position.y) / (mouse_position.x - character_position.x);
            souris = mouse_position - character_position;
            unitVect = new Vector2(souris.x / Mathf.Sqrt(souris.x * souris.x + souris.y * souris.y), souris.y / Mathf.Sqrt(souris.x * souris.x + souris.y * souris.y));

            cube = new GameObject();
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y + 10);
            cube.transform.localScale = new Vector3(1, 1);

            timePassed = Time.time;
            create = true;


        }
        else if (Input.GetMouseButton(0) && create)
        {
            timeDuring = Time.time - timePassed;

            cube.transform.localScale = new Vector3(timeDuring, 1);
            cube.transform.position = new Vector3(transform.position.x + cube.transform.localScale.x / 2, transform.position.y + 10);

            if (timeDuring > 3)
            {
                Destroy(cube);
                create = false;
                GameObject clone;
                clone = Instantiate(prefabs_bullet, new Vector3(transform.position.x + unitVect.x * 3, transform.position.y), transform.rotation);
                clone.GetComponent<Rigidbody2D>().AddForce(new Vector3(strengh * timeDuring * unitVect.x, strengh * timeDuring * unitVect.y));
            }
        }
        else if (create)
        {
            if (unitVect.x < 0)
            {
                dir = -1;
            }
            else
            {
                dir = 1;
            }
            GameObject clone;
            clone = Instantiate(prefabs_bullet, new Vector3(transform.position.x + 3 * dir + transform.localScale.x * dir, transform.position.y), transform.rotation);

            clone.GetComponent<Rigidbody2D>().AddForce(new Vector3(unitVect.x * strengh * timeDuring, unitVect.y * strengh * timeDuring));
            Destroy(cube);

            create = false;
        }

    }

}
