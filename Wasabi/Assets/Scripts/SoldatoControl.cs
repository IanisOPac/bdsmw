using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SoldatoControl : MonoBehaviour
{
    public float runSpeed = 40f;
    [SerializeField] private bool selected = false;
    public bool Selected
    {
        get { return selected; }
        set { selected = value; }
    }
    float xMove = 0f;
    int direction;
    Rigidbody2D rb;
    bool FacingRight = true;
    Animator anim; //Creer un Animator pour pouvoir lancer l'animation avec anim.Play
    int canMove;
    bool jumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Selected)
        {
            direction = FacingRight ? 1 : -1;
            canMove = jumping ? 0 : 1;
            xMove = Input.GetAxisRaw("Horizontal") * runSpeed * Time.deltaTime * canMove;

            transform.Translate(xMove * direction, 0, 0);

            if (Input.GetKeyDown(KeyCode.Space) && !jumping)
            {
                rb.AddForce(Vector2.up * 400f);
                jumping = true;
                anim.SetBool("Jumping", true);
            }

            anim.SetFloat("Speed", Mathf.Abs(xMove));

            Flip();
        }
    }

    public void Flip()
    {
        if (xMove < 0 && FacingRight || xMove > 0 && !FacingRight)
        {
            transform.Rotate(0, 180, 0);
            FacingRight = !FacingRight;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            jumping = false;
            anim.SetBool("Jumping", false);
        }
    }
}