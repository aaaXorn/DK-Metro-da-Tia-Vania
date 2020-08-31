using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrilMove : MonoBehaviour
{
    SpriteRenderer render;
    Rigidbody2D rigid;
    public float rightSpeed;
    public float leftSpeed;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (render.flipX == false)
        {
            rigid.velocity = new Vector2(rightSpeed, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(leftSpeed, rigid.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ParedeInv")
        {
            if (render.flipX == false)
            {
                render.flipX = true;
            }
            else
            {
                render.flipX = false;
            }
        }
        if (collision.gameObject.tag == "Morre")
        {
            Destroy(gameObject);
        }
    }
}
