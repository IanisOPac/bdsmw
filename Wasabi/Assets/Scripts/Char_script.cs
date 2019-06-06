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
    
    GameObject universe_laws;
    GameObject HUD;
    bool shoted = false;
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
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Translate(new Vector3(-0.1f, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(0.1f, 0));
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.A) && !shoted)
            {
                if (Time.time - universe_laws.GetComponent<Universe>().startTime <= 5)
                {
                    universe_laws.GetComponent<Universe>().startTime = Time.time - 5;
                }                
                CreateBomb();
                shoted = true;
            }
               
        }
    }

    void CreateBomb()
    {
        GameObject clone;
        clone = Instantiate(bomb, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 10));
        Physics2D.IgnoreCollision(clone.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>(), true);
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

    }

    private void UpdateHealth()
    {
        hp = Mathf.Clamp(hp, 0, hpmax);
        HUD.GetComponentInChildren<HealthDisplay>().SetText(NumEquipe, numWorms, hp, hpmax);
        HUD.GetComponentInChildren<HealthDisplay>().SetHp();
    }
    

}
