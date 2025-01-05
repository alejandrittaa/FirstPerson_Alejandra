using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{

    private Animator anim;
    private NavMeshAgent agent;
    private Player player;

    // Variables para detección del jugador
    public float rangoDeteccion = 7f; // Rango en el que detecta al jugador
    private bool persiguiendo = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //para que identifique donde está el player
        player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calcular la distancia entre el enemigo y el jugador
            float distancia = Vector3.Distance(transform.position, player.transform.position);

            if (distancia <= rangoDeteccion)
            {
                // Si el jugador está dentro del rango, empieza a perseguir
                persiguiendo = true;
            }
            else
            {
                // Si el jugador está fuera del rango, deja de perseguir
                persiguiendo = false;
            }

            if (persiguiendo)
            {
                // Dividimos la velocidad actual entre la velocidad máxima
                anim.SetFloat("velocity", agent.velocity.magnitude / agent.speed);

                // Hacer que persiga al jugador
                agent.SetDestination(player.gameObject.transform.position);
            }
            else
            {
                // Detener el movimiento del enemigo
                agent.ResetPath();
                anim.SetFloat("velocity", 0);
            }
        }
    }
}
