using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerUi : MonoBehaviour
{
    public Transform player; // Referência ao Transform do jogador
    public Vector3 offset; // Offset para ajustar a posição do Canvas

    void Update()
    {
        // Atualiza a posição do Canvas para seguir o jogador
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(player.position + offset);
        transform.position = screenPosition;
    }
}
