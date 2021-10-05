using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D meuRigidBody;
    private Animator meuAnimator;
    public float velocidade;
    public float forcaPulo;
    public bool olhandoParaEsquerda;
    public Transform groundCheck;
    private bool estaNoChao;

    // Start is called before the first frame update
    void Start()
    {
        // coleta o componente de regidbody 
        meuRigidBody = GetComponent<Rigidbody2D>();
        meuAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // coleta o input do movimento horizontal
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0 && olhandoParaEsquerda)
        {
            Flip();
        } else if (horizontal < 0 && !olhandoParaEsquerda)
        {
            Flip();
        }

        float velocidadeY = meuRigidBody.velocity.y;

        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            meuRigidBody.AddForce(new Vector2(
                    0.0f, 
                    forcaPulo
                )
            );
        }

        meuRigidBody.velocity = new Vector2(horizontal * velocidade, velocidadeY);
        meuAnimator.SetInteger("h", (int) horizontal);
        meuAnimator.SetBool("isGrounded", estaNoChao);
    }

    void FixedUpdate() 
    {
        // verifica a colisão
        estaNoChao = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    // função para flipar o personagem
    void Flip()
    {
        // inverte a variável
        olhandoParaEsquerda = !olhandoParaEsquerda;
        // inverte o sinal do scale x
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}
