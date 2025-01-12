using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{


    private Animator anim;
    private NavMeshAgent agent;
    private Player player;

    //cosas en referencia al player
    public float rangoDeteccion = 7f; // Rango en el que detecta al jugador
    public float rangoAtaque = 1.5f; // Rango para realizar el ataque
    public int da�o = 10; // Da�o que el ataque hace al jugador
    public float tiempoEntreAtaques = 1.5f; // Tiempo entre ataques
    private bool persiguiendo = false;
    private bool puedeAtacar = true; // Control de cooldown de ataque

    //cosas en referencia al enemigo
    public int vidaMaxima = 30; // Vida total del enemigo
    private int vidaActual;
    private bool estaMuerto = false;

    public Rigidbody[] ragdollPartes; // Lista de todos los rigidbodies del modelo
    public Collider[] collidersRagdoll; // Lista de colliders para el ragdoll

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Para identificar d�nde est� el player
        player = GameObject.FindObjectOfType<Player>();

        // Inicializar la vida y desactivar el ragdoll
        vidaActual = vidaMaxima;
        DesactivarRagdoll();
    }

    void Update()
    {
        if (player != null)
        {
            // Calcular la distancia entre el enemigo y el jugador
            float distancia = Vector3.Distance(transform.position, player.transform.position);

            if (distancia <= rangoDeteccion && distancia > rangoAtaque)
            {
                // Si el jugador est� dentro del rango de detecci�n pero fuera del rango de ataque, perseguirlo
                persiguiendo = true;
            }
            else if (distancia <= rangoAtaque)
            {
                // Si el jugador est� dentro del rango de ataque, detenerse y atacar
                persiguiendo = false;
                AtacarJugador();
            }
            else
            {
                // Si el jugador est� fuera del rango de detecci�n, dejar de perseguir
                persiguiendo = false;
            }

            if (persiguiendo)
            {
                // Dividimos la velocidad actual entre la velocidad m�xima
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

            // Reproducir la animaci�n de ataque
            anim.SetTrigger("attack");

            // Reducir la vida del jugador
            player.ReducirVida(da�o);

            // Iniciar un cooldown antes del pr�ximo ataque
            StartCoroutine(CooldownAtaque());
        }
    }

    IEnumerator CooldownAtaque()
    {
        puedeAtacar = false;
        yield return new WaitForSeconds(tiempoEntreAtaques);
        puedeAtacar = true;
    }

    public void RecibirDa�o(int cantidad)
    {
        if (estaMuerto) return;

        vidaActual -= cantidad;
        Debug.Log($"Enemigo {name} recibi� da�o. Vida restante: {vidaActual}");

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        estaMuerto = true;
        ActivarRagdoll();

        // Destruir el objeto despu�s de 5 segundos
        Destroy(this.gameObject, 5f);
    }

    void ActivarRagdoll()
    {
        // Habilitar todos los rigidbodies y colliders del ragdoll
        foreach (Rigidbody rb in ragdollPartes)
        {
            rb.isKinematic = false;
        }

        foreach (Collider col in collidersRagdoll)
        {
            col.enabled = true;
        }

        // Desactivar el NavMeshAgent o cualquier otro movimiento del enemigo
        if (TryGetComponent(out NavMeshAgent agent))
        {
            agent.enabled = false;
        }

        // Desactivar el collider principal del enemigo
        if (TryGetComponent(out Collider mainCollider))
        {
            mainCollider.enabled = false;
        }

        // Desactivar animaciones
        if (TryGetComponent(out Animator anim))
        {
            anim.enabled = false;
        }
    }

    void DesactivarRagdoll()
    {
        // Desactivar todos los rigidbodies y colliders del ragdoll
        foreach (Rigidbody rb in ragdollPartes)
        {
            rb.isKinematic = true;
        }

        foreach (Collider col in collidersRagdoll)
        {
            col.enabled = false;
        }
    }
}
