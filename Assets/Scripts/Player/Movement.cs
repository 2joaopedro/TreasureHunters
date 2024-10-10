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
    public float speed;

    // Pulo
    public float JumpForce;
    private bool isJumping;
    private bool doubleJump;

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
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime);

        // Controle de animação e direção do personagem
        if(direction.magnitude > 0f) 
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
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                isJumping = true;
                doubleJump = true;
            }
            else if(doubleJump)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = false; // Desativa o duplo pulo
            }
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
