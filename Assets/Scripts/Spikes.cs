using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    private bool isDead;
    private float deathTimer; // Timer para contar o tempo
    public GameObject player;
    public GameObject imageDead;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false; // Inicializa a variável isDead
        imageDead.SetActive(false); // Esconde a imagem de morte inicialmente
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se o jogador está morto
        if (isDead)
        {
            deathTimer += Time.deltaTime; // Incrementa o timer com o tempo decorrido

            // Se o tempo de morte atingir 5 segundos, reinicia a cena
            if (deathTimer >= 1f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarrega a cena
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player") && !isDead) // Verifica se o jogador não está morto
        {
            isDead = true; // Marca que o jogador está morto
            imageDead.SetActive(true); // Exibe a imagem de morte
            Destroy(player); // Destrói o jogador
        }   
    }
}
