using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Importa o SceneManager

public class NextLevel : MonoBehaviour
{
    public GameObject imageNextLevel;
    bool isActive;

    void Update() {
        if(Input.GetKeyDown(KeyCode.E)) {
            if(isActive) {
                LoadNextLevel();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.layer == 9){
            isActive =! isActive;
            imageNextLevel.SetActive(isActive);
        }   
    }
    void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.gameObject.layer == 9){
            isActive =! isActive;
            imageNextLevel.SetActive(isActive);
        }   
    }
   void LoadNextLevel() {
        // Carrega a próxima cena, que pode ser pelo nome ou índice
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
}
