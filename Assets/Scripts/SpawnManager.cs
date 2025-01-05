using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemigoPrefab; // Prefab del enemigo
    public Transform puntoSpawn; // Punto específico donde aparecerán los enemigos
    public int cantidadEnemigos = 3; // Cantidad de enemigos a spawnear
    private bool yaSpawneado = false; // Para evitar que se spawneen varias veces
    public Transform[] puntosSpawn; // Array de puntos específicos

    void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto que entra en la zona es el jugador
        if (other.CompareTag("Player") && !yaSpawneado)
        {
            SpawnEnemigos();
            yaSpawneado = true; // Asegurar que solo se spawneen una vez y que por más que vuelvas a la zona, ya esté vacia por que lo has matado
        }
    }

    void SpawnEnemigos()
    {
        foreach (Transform punto in puntoSpawn)
        {
            for (int i = 0; i < cantidadEnemigos; i++)
            {
                Instantiate(enemigoPrefab, punto.position, punto.rotation);
            }
        }
    }
}
