using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarZona : MonoBehaviour
{
    [SerializeField] private Collider zonaBloqueada; // Collider que separa la zona 1 de la zona 2

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra es el jugador
        if (other.CompareTag("Player"))
        {
            // Desactivar el collider o convertirlo en trigger
            if (zonaBloqueada != null)
            {
                zonaBloqueada.isTrigger = true;
            }

            // Eliminar este objeto (el objeto interactivo)
            Destroy(this.gameObject);

            Debug.Log("Zona desbloqueada. Puedes avanzar.");
        }
    }
}
