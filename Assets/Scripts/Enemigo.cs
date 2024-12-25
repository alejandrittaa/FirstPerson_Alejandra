using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{

    private Animator anim;
    [SerializeField] private NavMeshAgent agent;
    private Player player;

    void Start()
    {
        anim = GetComponent<Animator>();

        //para que identifique donde est� el player
        player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        //dividimos la velocidad actual entre la velocidad m�xima
        anim.SetFloat("velocity", agent.velocity.magnitude / agent.speed);

        //para que vaya a por el player
        agent.SetDestination(player.gameObject.transform.position);
    }
}
