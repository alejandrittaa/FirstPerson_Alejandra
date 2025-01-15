using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemigoPrefab; // Prefab del enemigo
    public Transform[] puntosSpawn; // Array de puntos específicos donde aparecerán los enemigos
    public int cantidadEnemigosPorPunto = 1; // Cantidad de enemigos por cada punto de spawn
    public bool yaSpawneado = false; // Para evitar que se spawneen varias veces

    private List<GameObject> enemigosGenerados = new List<GameObject>(); // Lista de enemigos generados
    public Player player; // Asigna el jugador desde el Inspector
    void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto que entra en la zona es el jugador
        if (other.CompareTag("Player") && !yaSpawneado)
        {
            SpawnEnemigos();
            yaSpawneado = true; // Asegurar que solo se spawneen una vez
        }
    }

    public void SpawnEnemigos()
    {
        if (yaSpawneado)
        {
            Debug.Log("Enemigos ya fueron generados. No se generarán nuevamente.");
            return;
        }

        Debug.Log("Generando enemigos...");

        yaSpawneado = true; // Marcar como spawneado ANTES de generar enemigos

        foreach (Transform punto in puntosSpawn) // Recorrer todos los puntos de spawn
        {
            for (int i = 0; i < cantidadEnemigosPorPunto; i++) // Instanciar x enemigos por punto (al final he puesto solo 1 por punto)
            {
                // Guarda la referencia del enemigo generado
                GameObject enemigo = Instantiate(enemigoPrefab, punto.position, punto.rotation);

                // Añadir a la lista para rastrear
                enemigosGenerados.Add(enemigo);
            }
        }
    }

    //para poder reiniciar el spawn y que los enemigos se puedan volver a crear cuando se reinicie el nivel
    public void ReiniciarSpawn()
    {
        Debug.Log("Reiniciando Spawn...");

        yaSpawneado = false; // Permitir que los enemigos vuelvan a generarse

        // Iterar y eliminar todos los enemigos generados previamente
        foreach (GameObject enemigo in enemigosGenerados)
        {
            if (enemigo != null)
            {
                Debug.Log($"Eliminando enemigo: {enemigo.name}");

                // Verifica si el enemigo tiene un NavMeshAgent
                if (enemigo.TryGetComponent<UnityEngine.AI.NavMeshAgent>(out var agent))
                {
                    agent.enabled = false; // Desactiva el NavMeshAgent
                }

                Destroy(enemigo); // Destruye el enemigo
            }

        }
        Debug.Log("Reiniciando Spawn: Enemigos eliminados y yaSpawneado = false.");
        enemigosGenerados.Clear(); // Limpia la lista para evitar referencias rotas

        //reiniciar la vida del jugador a su vida máxima
        // Restablecer la vida del jugador
        if (player != null)
        {
            player.RestablecerVida();
            Debug.Log("Vida player restablecida");
        }
        else
        {
            Debug.LogWarning("El jugador no está asignado en el SpawnManager.");
        }

    }
}
