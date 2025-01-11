using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{


    private Animator anim;
    private NavMeshAgent agent;
    private Player player;

    
    public float rangoDeteccion = 7f; // Rango en el que detecta al jugador
    public float rangoAtaque = 1.5f; // Rango para realizar el ataque
    public int daño = 10; // Daño que el ataque hace al jugador
    public float tiempoEntreAtaques = 1.5f; // Tiempo entre ataques
    private bool persiguiendo = false;
    private bool puedeAtacar = true; // Control de cooldown de ataque

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Para identificar dónde está el player
        player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calcular la distancia entre el enemigo y el jugador
            float distancia = Vector3.Distance(transform.position, player.transform.position);

            if (distancia <= rangoDeteccion && distancia > rangoAtaque)
            {
                // Si el jugador está dentro del rango de detección pero fuera del rango de ataque, perseguirlo
                persiguiendo = true;
            }
            else if (distancia <= rangoAtaque)
            {
                // Si el jugador está dentro del rango de ataque, detenerse y atacar
                persiguiendo = false;
                AtacarJugador();
            }
            else
            {
                // Si el jugador está fuera del rango de detección, dejar de perseguir
                persiguiendo = false;
            }

            if (persiguiendo)
            {
                // Dividimos la velocidad actual entre la velocidad máxima
                anim.SetFloat("velocity", agent.velocity.magnitude / agent.speed);

                // Hacer que persiga al jugador
                agent.SetDestination(player.transform.position);
                agent.isStopped = false;
            }
            else
            {
                // Detener el movimiento del enemigo
                agent.ResetPath();
                anim.SetFloat("velocity", 0);
                agent.isStopped = true;
            }
        }
    }

    void AtacarJugador()
    {
        if (puedeAtacar)
        {
            // Detener el movimiento del enemigo
            agent.isStopped = true;

            // Reproducir la animación de ataque
            anim.SetTrigger("attack");

            // Reducir la vida del jugador
            player.ReducirVida(daño);

            // Iniciar un cooldown antes del próximo ataque
            StartCoroutine(CooldownAtaque());
        }
    }

    IEnumerator CooldownAtaque()
    {
        puedeAtacar = false;
        yield return new WaitForSeconds(tiempoEntreAtaques);
        puedeAtacar = true;
    }
}
