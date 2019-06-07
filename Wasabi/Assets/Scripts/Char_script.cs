using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char_script : MonoBehaviour
{

    [SerializeField] GameObject bomb;
    
    private int numEquipe;
    private int numWorms;
    public int NumWorms
    {
        get { return numWorms; }
        set { numWorms = value; }
    }
    public int NumEquipe
    {
        get { return numEquipe; }
        set { numEquipe = value; }
    }

    [SerializeField] private int hp;
    [SerializeField] private int hpmax;
    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public int HpMax
    {
        get { return hpmax; }
        set { hpmax = value; }
    }

    [SerializeField] private bool selected = false;
    public bool Selected
    {
        get { return selected; }
        set { selected = value; }
    }
    float timePassed, timeDuring;
    int strengh = 400;
    GameObject universe_laws;
    GameObject HUD;
    Vector3 mouse_position;
    float mouse_distance;
    float angle_souris;
    bool create = false;
    public bool Create
    {
        get { return create; }
        set { create = value; }
    }
    bool shoted = false;
    int dir;
    Vector2 unitVect, souris;
    [SerializeField] GameObject prefabs_bullet;
    GameObject clone;
    public bool Shoted
    {
        get { return shoted; }
        set { shoted = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        universe_laws = GameObject.Find("universe_laws");
        HUD = GameObject.Find("HUD");
    }

    private void OnMouseOver()
    {
        HUD.GetComponentInChildren<HealthDisplay>().SetEnnemyInfo(NumEquipe, Hp, HpMax, NumWorms);
    }

    private void OnMouseExit()
    {
        HUD.GetComponentInChildren<HealthDisplay>().CleanEnnemyInfo();
    }

    // Update HUD, Cam, and selected element
    public void ChangeSelected()
    {
        universe_laws.GetComponent<Universe>().Selected = this.gameObject;
        Camera.main.GetComponent<CameraFollow>().Target = this.transform;
        HUD.GetComponentInChildren<HealthDisplay>().SetText(NumEquipe, numWorms, hp, hpmax);
    }


    public void SetSelected()
    {
        universe_laws.GetComponent<Universe>().Selected = transform.gameObject;
    }

    // Return the actual selected worm
    public Char_script GetSelected()
    {
        return universe_laws.GetComponent<Universe>().Selected.GetComponent<Char_script>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Selected)
        {
            ChangeSelected();                           
            CreateBomb();
            if (!shoted)
            {
                mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                angle_souris = Mathf.Atan((mouse_position.y - transform.position.y) / (mouse_position.x - transform.position.x));
                mouse_distance = (mouse_position.y - transform.position.y) / (mouse_position.x - transform.position.x);
                souris = mouse_position - transform.position;
                unitVect = new Vector2(souris.x / Mathf.Sqrt(souris.x * souris.x + souris.y * souris.y), souris.y / Mathf.Sqrt(souris.x * souris.x + souris.y * souris.y));
                if (Input.GetKey(KeyCode.A) && !create)
                {                    
                    timePassed = Time.time;
                    create = true;

                }
                else if (Input.GetKey(KeyCode.A) && create)
                {                
                    timeDuring = Time.time - timePassed;
                    if (timeDuring > 3)
                    {
                        create = false;
                        clone = Instantiate(prefabs_bullet, new Vector3(transform.position.x + unitVect.x * 3, transform.position.y), transform.rotation);
                        clone.GetComponent<Rigidbody2D>().AddForce(new Vector3(strengh * timeDuring * unitVect.x, strengh * timeDuring * unitVect.y));
                        shoted = true;
                    }
                }
                else if (create)
                {
                    if (Time.time - universe_laws.GetComponent<Universe>().startTime <= 5)
                    {
                        universe_laws.GetComponent<Universe>().startTime = Time.time - 5;
                    }
                    if (unitVect.x < 0)
                    {
                        dir = -1;
                    }
                    else
                    {
                        dir = 1;
                    }
                    clone = Instantiate(prefabs_bullet, new Vector3(transform.position.x + unitVect.x * 3, transform.position.y), transform.rotation);
                    clone.GetComponent<Rigidbody2D>().AddForce(new Vector3(unitVect.x * strengh * timeDuring, unitVect.y * strengh * timeDuring));
                    shoted = true;
                }
            }
            
        }
    }

    void CreateBomb()
    {
        /*GameObject clone;
        clone = Instantiate(bomb, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 10));
        Physics2D.IgnoreCollision(clone.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>(), true);*/
    }

    public void TakeDamage(int damages)
    {
        hp -= damages;
        UpdateHealth();
        if (hp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        // TO DO
    }

    private void UpdateHealth()
    {
        hp = Mathf.Clamp(hp, 0, hpmax);
        HUD.GetComponentInChildren<HealthDisplay>().SetText(NumEquipe, numWorms, hp, hpmax);
        HUD.GetComponentInChildren<HealthDisplay>().SetHp();
    }
    

}
