using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerControl : NetworkBehaviour
{

    public float speed;
    public float jumpSpeed;

    Rigidbody2D rb;
    bool canJump = true;
    bool jumping = false;
    float xMove = 0f;
    public bool facingRight = true;
    int direction;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        direction = facingRight ? 1 : -1;
        xMove = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        transform.Translate(xMove * direction, 0, 0);

        if (jumping)
        {
            xMove /= 2f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            jumping = true;
            rb.AddForce(Vector2.up * jumpSpeed);
        }

        Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = true;
            jumping = false;
        }
    }

    private void Flip()
    {
        if (xMove < 0 && facingRight || xMove > 0 && !facingRight)
        {
            transform.Rotate(0, 180, 0);
            facingRight = !facingRight;
        }
    }
}
