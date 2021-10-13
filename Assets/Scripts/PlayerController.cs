using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    public float Speed;
    public float JumpForce;
    public bool IsJumping;
    public float DashForce;

    // Start is called before the first frame update
    void Start()
    {
        // coleta o componente Rigidbody2D
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // chama a função que movementa do personagem
        Move();
        // chama a função que ativa o pulo do personagem
        Jump();
    }

    // movimenta o player
    void Move()
    {
        // aplica coleta movimento com base ativação do pesonagem com o teclado
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        // aplica a movimentação no personagem com os valores inputados pelo teclado do jogador 
        transform.position += movement * Time.deltaTime * Speed;
        // se o personagem for para a direita
        if(Input.GetAxis("Horizontal") > 0) 
        {
            // altera para true a variável da animação
            anim.SetBool("walk", true);
            // ajusta o lado da sprite
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        // se o personagem for para a esquerda
        } else if(Input.GetAxis("Horizontal") < 0) {
            // altera para true a variável da animação
            anim.SetBool("walk", true);
            // ajusta o lado da sprite
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        // senão
        } else {
            // altera para false a variável da animação
            anim.SetBool("walk", false);
        }
    }

    // pulo do player
    void Jump()
    {
        // verifica que o Jump foi apertado e se o personagem não está pulando
        if (Input.GetButtonDown("Jump") && !IsJumping)
        {
            // aplica a força do pulo
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }

    // chamada quando alguma colisão de entrada é detectada
    private void OnCollisionEnter2D(Collision2D collision) {
        // verifica se a layer que colidiu é a 8
        if (collision.gameObject.layer == 8)
        {
            // altera para false a variável que controla se o player está pulando
            IsJumping = false;
            // altera para false a variável da animação
            anim.SetBool("jump", false);
        }
    }
    // chamada quando alguma colisão de saída é detectada
    private void OnCollisionExit2D(Collision2D collision) {
        // verifica se a layer que colidiu é a 8
        if (collision.gameObject.layer == 8)
        {
            // altera para true a variável que controla se o player está pulando
            IsJumping = true;
            // altera para true a variável da animação
            anim.SetBool("jump", true);
        }
    }
}
