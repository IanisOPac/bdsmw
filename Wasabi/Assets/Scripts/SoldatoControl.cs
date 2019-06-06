using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace SoldatoControl { 
    public class SoldatoControl : MonoBehaviour
    {
        float xMove = 0f;
        public bool grounded = true;
        public float runSpeed = 40f;
        int direction;
        private Rigidbody2D rb;
        private bool FacingRight = true;
        public Animator anim; //Creer un Animator pour pouvoir lancer l'animation avec anim.Play
        int canMove;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            anim.SetBool("Idle", true);
        }
        // Update is called once per frame
        void Update()
        {
            direction = FacingRight ? 1 : -1;
            canMove = grounded ? 1 : 0;
            xMove = Input.GetAxisRaw("Horizontal") * runSpeed * Time.deltaTime;
            transform.Translate(xMove * direction * canMove, 0, 0);

            if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
            {
                rb.AddForce(Vector2.up * 400f);
                
            }
            AnimState();
            Flip();
        }

        void AnimState()
        {
            if (Mathf.Abs(xMove) == 0 && grounded)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Idle", true);
            }
            if (Mathf.Abs(xMove) > 0 || Mathf.Abs(xMove) < 0 && canMove == 1)
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", true);
            }
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);
                anim.Play("test");
                grounded = false;
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
                    grounded = true;
            
            }
        }
    }
}