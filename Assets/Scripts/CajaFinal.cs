using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaFinal : MonoBehaviour
{

    [SerializeField] private Collider zonaBloqueada; // Collider que separa la zona 1 de la zona 2
    public bool desbloquearCaja = false;
    public GameObject PantallaFin;

    /*private Outline outline;
    private bool enemigosSpawneados = false; // Variable para controlar si los enemigos han sido spawneados

    private void Start()
    {
        // Obtiene el componente Outline
        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        // Comprueba si los enemigos han sido spawneados y todos están muertos
        if (enemigosSpawneados && TodosLosEnemigosMuertos())
        {
            if (outline != null) // Verifica si el componente sigue siendo válido
            {
                outline.enabled = true;
                Debug.Log("Outline habilitado.");
            }
        }
    }

    // Método para indicar que los enemigos han sido spawneados
    public void MarcarEnemigosSpawneados()
    {
        enemigosSpawneados = true;
        Debug.Log("Enemigos spawneados.");
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Comprobando si se puede abrir el cofre");

        // Verificar si el objeto que entra es el jugador
        if (other.CompareTag("Player") && TodosLosEnemigosMuertos())
        {
            Debug.Log("Has cogido el cofre");

            // Desactivar el collider o convertirlo en trigger
            if (zonaBloqueada != null)
            {
                zonaBloqueada.isTrigger = true;
            }

            // Eliminar este objeto (el objeto interactivo)
            Destroy(this.gameObject);
            // Activa la pantalla de fin
            PantallaFin.SetActive(true);

        }
        else if (!TodosLosEnemigosMuertos())
        {
            Debug.Log("No puedes recoger la caja hasta que todos los enemigos hayan muerto.");
        }
    }

    private bool TodosLosEnemigosMuertos()
    {
        Enemigo[] enemigos = FindObjectsOfType<Enemigo>();

        foreach (Enemigo enemigo in enemigos)
        {
            // Ignorar el enemigo base usando su tag
            /*if (enemigo.CompareTag("EnemigoBase"))
            {
                continue;
            }*/

            if (enemigo != null && enemigo.gameObject.activeInHierarchy && !enemigo.EstaMuerto())
            {
                return false; // Si hay algún enemigo vivo, devuelve false
            }
        }

        return true; // Si no hay enemigos vivos, devuelve true
    }
}
