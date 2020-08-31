using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggAnima : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    public float tempoAnim;
    float tempoAtual = 0;
    public Player Plr;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        tempoAtual += Time.deltaTime;
        if (tempoAtual > tempoAnim-1 && tempoAtual < tempoAnim)
        {
            anim.SetBool("Spawnando", true);
        }
        else if (tempoAtual > tempoAnim)
        {
            anim.SetBool("Spawnando", false);
            tempoAtual = 0;
        }

        if(Plr.Venceu == true)
        {
            anim.SetBool("Venceu", true);
            rigid.gravityScale = 1;
        }
    }
}
