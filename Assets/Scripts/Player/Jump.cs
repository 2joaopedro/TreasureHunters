using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private bool isJumping;
    public float JumpForce;
    private bool doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")) 
        {
            if(!isJumping) 
            {
                rig.velocity = new Vector2(rig.velocity.x, JumpForce); // Usa rig.velocity para pulo mais controlado
                isJumping = true; // Agora o personagem está no ar
                doubleJump = true; // Permitir o segundo pulo
            }
            else 
            {
                if(doubleJump) 
                {
                    rig.velocity = new Vector2(rig.velocity.x, JumpForce); // Segundo pulo
                    doubleJump = false; // Desabilitar o segundo pulo
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.layer == 8) // Supondo que o chão está na camada 8
        {
            isJumping = false; // Volta ao estado de não estar pulando quando toca o chão
        }   
    }
}
