using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{

    private Animator anim;
    [SerializeField] private NavMeshAgent agent;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //dividimos la velocidad actual entre la velocidad máxima
        anim.SetFloat("velocity", agent.velocity.magnitude / agent.speed);
    }
}
