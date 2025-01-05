using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{

    private Animator anim;
    private NavMeshAgent agent;
    private Player player;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //para que identifique donde está el player
        player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        //dividimos la velocidad actual entre la velocidad máxima
        anim.SetFloat("velocity", agent.velocity.magnitude / agent.speed);

        //para hacer que persiga al player
        agent.SetDestination(player.gameObject.transform.position);
    }
}
