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

    float doubleTapTime;
    KeyCode lastKeyCode;
    public float dashSpeed;
    private float dashCount;
    public float startDashCount;
    private int side;
    private float dashCooldown;
    private bool estaNoDash;

    // Start is called before the first frame update
    void Start()
    {
        // coleta o componente de regidbody 
        meuRigidBody = GetComponent<Rigidbody2D>();
        meuAnimator = GetComponent<Animator>();

        dashCount = startDashCount;
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

        if (Input.GetButtonDown("Fire3") && estaNoChao)
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
        
        // dash
        dashCooldown -= Time.deltaTime;
        if (side == 0)
        {
            if (Input.GetButtonDown("Fire1") && dashCooldown <= 0)
            {
                side = 1;
                dashCount = startDashCount;
                estaNoDash = true;
            } else {
                estaNoDash = false;
            }
        }
        else
        {
            if (dashCount <= 0)
            {
                dashCount = startDashCount;
                side = 0;
            } else {
                dashCount -= Time.deltaTime;
                if (olhandoParaEsquerda)
                {
                    meuRigidBody.velocity = Vector2.left * dashSpeed;
                } else
                {
                    meuRigidBody.velocity = Vector2.right * dashSpeed;
                }
                dashCooldown = 0.8f;
            }
        }
        meuAnimator.SetBool("isDashing", estaNoDash);

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
