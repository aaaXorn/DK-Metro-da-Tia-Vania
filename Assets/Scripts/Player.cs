using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator anim;
    SpriteRenderer render;
    Rigidbody2D rigid;
    public float speedX;
    public float speedY;
    float inputH;
    float inputV;
    public float stairDistance;
    public LayerMask stairCheck;
    bool grounded;
    bool stairClimb;
    bool hammer;
    float tempoAtual = 0;
    bool Morreu;
    public float tempoMartelo = 10;
    float tempoMorrendo = 0;
    public float tempoMorte = 4;
    public bool Venceu;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && grounded == true)//pulo
        {
            anim.SetTrigger("Jump");
            rigid.AddForce(new Vector2(0, 150));
            grounded = false;
            anim.SetBool("Airborne", true);
        }
        if (Morreu == true || Venceu == true)
        {
            speedX = 0;
            speedY = 0;
            if (Morreu == true)
            {
                anim.SetBool("GameOver", true);
            }
            else if (Venceu == true)
            {
                anim.SetBool("GameOver", true);
                anim.SetBool("Win", true);
            }
            tempoMorrendo += Time.deltaTime;
            if (tempoMorrendo > tempoMorte)
            {
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            }
        }
    }

    private void FixedUpdate()
    {
        inputH = Input.GetAxis("Horizontal");

        rigid.velocity = new Vector2(inputH * speedX, rigid.velocity.y);//movimento horizontal
        anim.SetFloat("MovementH", Mathf.Abs(inputH));
        if (inputH > 0)
        {
            render.flipX = false;
        }
        else if (inputH < 0)
        {
            render.flipX = true;
        }

        if (hammer == true)
        {
            UpdateMartelo();
        }
        else
        {
            UpdateEscada();
        }
    }

    private void UpdateMartelo()
    {
        tempoAtual += Time.deltaTime;
        if (tempoAtual < tempoMartelo)
        {
            anim.SetBool("Martelo", true);
        }
        else
        {
            anim.SetBool("Martelo", false);
            hammer = false;
        }
    }

    private void UpdateEscada()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, stairDistance, stairCheck);//detecção da layer Escada
        if ((hitInfo.collider) != null)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                inputV = Input.GetAxis("Vertical");
                rigid.velocity = new Vector2(rigid.position.x, inputV * speedY);
                rigid.gravityScale = 0;
                anim.SetBool("Stair", true);
            }
        }
        else
        {
            rigid.gravityScale = 1;
            anim.SetBool("Stair", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")//para o pulo funcionar
        {
            grounded = true;
            anim.SetBool("Airborne", false);
        }
        else if (collision.gameObject.tag == "Martelo")
        {
            hammer = true;
        }
        else if (collision.gameObject.tag == "Barril")
        {
            float xcollision = collision.gameObject.transform.position.x;
            float x = gameObject.transform.position.x;
            if (hammer == true)
            {
                if (x > xcollision && render.flipX == true ||
                    x < xcollision && render.flipX == false ||
                    x == xcollision)
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    Morreu = true;
                }
            }
            else
            {
                Morreu = true;
            }
        }
        else if (collision.gameObject.tag == "Morre")
        {
            Morreu = true;
        }
        else if (collision.gameObject.tag == "Sonic")
        {
            Venceu = true;
        }
    }
}
