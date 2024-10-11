using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer sprite;
    private Vector2 direction;
    private Vector2 lastDirection = Vector2.zero;

    // Movimento
    public float speed = 5f;
    public float airControlSpeed = 0.5f; // Menor velocidade de controle no ar

    // Pulo
    public float jumpForce = 10f;
    private bool isJumping;
    private bool doubleJump;

    // Ajustes de física
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();  // Separando o movimento para uma função clara
        HandleJump();      // Separando o pulo para uma função clara
    }

    // Lógica para o movimento do personagem
    void HandleMovement() 
    {
        // Pega a direção horizontal
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), rig.velocity.y);

        // Se o jogador estiver no chão, usa velocidade normal
        if(!isJumping)
        {
            rig.velocity = new Vector2(direction.x * speed, rig.velocity.y);
        }
        // Se o jogador estiver no ar, diminui o controle
        else
        {
            rig.velocity = new Vector2(direction.x * speed * airControlSpeed, rig.velocity.y);
        }

        // Controle de animação e direção do personagem
        if(direction.x != 0) 
        {
            lastDirection = direction;
            anim.SetInteger("Base", 1); // Jogador está se movendo

            if(direction.x > 0f)
            {
                sprite.flipX = false; // Virado para a direita
            } 
            else if (direction.x < 0f) 
            {
                sprite.flipX = true; // Virado para a esquerda
            } 
        }
        else 
        {
            // Jogador está parado
            anim.SetInteger("Base", 0);
            sprite.flipX = lastDirection.x < 0f; // Mantém a direção da última movimentação
        }
    }

    // Lógica para o pulo e duplo pulo
    void HandleJump() 
    {
        if(Input.GetButtonDown("Jump")) 
        {
            if(!isJumping) 
            {
                rig.velocity = new Vector2(rig.velocity.x, jumpForce); // Definindo a velocidade de pulo diretamente
                isJumping = true;
                doubleJump = true;
            }
            else if(doubleJump)
            {
                rig.velocity = new Vector2(rig.velocity.x, jumpForce); // Permite o duplo pulo
                doubleJump = false; // Desativa o duplo pulo
            }
        }

        // Aplicando física para um pulo mais responsivo
        if (rig.velocity.y < 0) // Quando o personagem está caindo
        {
            rig.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rig.velocity.y > 0 && !Input.GetButton("Jump")) // Quando o jogador pula mais baixo ao soltar o botão
        {
            rig.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // Detecção de colisão para verificar se o jogador tocou o chão
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.layer == 8) // Verifica se o jogador tocou o chão (layer 8)
        {
            isJumping = false; // O jogador está no chão
        }   
    }

    // Verifica quando o jogador sai do chão
    void OnCollisionExit2D(Collision2D collision) 
    {
        if(collision.gameObject.layer == 8) // Quando o jogador deixa de colidir com o chão
        {
            isJumping = true; // O jogador está no ar
        }
    }
}
