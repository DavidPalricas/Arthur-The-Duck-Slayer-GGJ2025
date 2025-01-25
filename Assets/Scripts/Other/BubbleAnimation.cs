using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAnimation : MonoBehaviour
{
    public float amplitude = 0.03f; // Pequena distância vertical
    public float frequencia = 2f; // Velocidade
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position; // Armazena a posição inicial
    }

    void Update()
    {
        // Calcula o deslocamento Y usando o seno
        float yOffset = Mathf.Sin(Time.time * frequencia) * amplitude;
        transform.position = posicaoInicial + new Vector3(0, yOffset, 0);
    }
}
