using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char_script : MonoBehaviour
{

    [SerializeField] GameObject bomb;
    private int numEquipe;

    public void setNumEquipe(int num)
    { numEquipe = num; }

    [SerializeField] private int hp;
    [SerializeField] private int hpmax;

    [SerializeField] private Camera cam;
    GameObject floor;
    Char_script selected;
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        floor = GameObject.Find("floor");
    }

    private void OnMouseOver()
    {
        Debug.Log(hp + "/" + hpmax);
    }


    private void OnMouseDown()
    {
        floor.GetComponent<SelectWorms>().SetSelected(this.gameObject);
    }

    public Char_script GetSelected()
    {
        return floor.GetComponent<SelectWorms>().GetSelected();
    }
    // Update is called once per frame
    void Update()
    {
        selected = GetSelected();
        if (selected.Equals(this))
        {
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Translate(new Vector3(-0.1f, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                //ddanim.Play("Soldat-Move");
                transform.Translate(new Vector3(0.1f, 0));
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.A))
            {
                CreateBomb();
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
            Debug.Log("dead");
        }
    }

    private void UpdateHealth()
    {
        hp = Mathf.Clamp(hp, 0, hpmax);
    }

}
