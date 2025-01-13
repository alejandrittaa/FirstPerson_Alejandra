using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemigoPrefab; // Prefab del enemigo
    public Transform[] puntosSpawn; // Array de puntos específicos donde aparecerán los enemigos
    public int cantidadEnemigosPorPunto = 1; // Cantidad de enemigos por cada punto de spawn
    private bool yaSpawneado = false; // Para evitar que se spawneen varias veces

    void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto que entra en la zona es el jugador
        if (other.CompareTag("Player") && !yaSpawneado)
        {
            SpawnEnemigos();
            yaSpawneado = true; // Asegurar que solo se spawneen una vez
        }
    }

    void SpawnEnemigos()
    {
        foreach (Transform punto in puntosSpawn) // Recorrer todos los puntos de spawn
        {
            for (int i = 0; i < cantidadEnemigosPorPunto; i++) // Instanciar x enemigos por punto (al final he puesto solo 1 por punto)
            {
                Instantiate(enemigoPrefab, punto.position, punto.rotation);
            }
        }
    }

    //para poder reiniciar el spawn y que los enemigos se puedan volver a crear cuando se reinicie el nivel
    public void ReiniciarSpawn()
    {

        yaSpawneado = false; // Permitir que los enemigos vuelvan a generarse

        // Busca y elimina a todos los enemigos generados
        Enemigo[] enemigos = FindObjectsOfType<Enemigo>();
        foreach (var enemigo in enemigos)
        {
            // Verifica si el enemigo tiene un NavMeshAgent
            if (enemigo.TryGetComponent<UnityEngine.AI.NavMeshAgent>(out var agent))
            {
                //agent.ResetPath(); // Detén cualquier movimiento pendiente
                agent.enabled = false; // Desactiva el NavMeshAgent
            }

            Destroy(enemigo.gameObject); // Destruye el enemigo
        }
    }
}
