using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{

    public float rangoDisparo = 50f; // Rango máximo del arma
    public int daño = 20; // Daño que inflige el arma
    public ParticleSystem efectoDisparo; // Efecto visual del disparo (opcional)
    public AudioSource sonidoDisparo; // Sonido del disparo (opcional)

    void Update()
    {
        // Detectar si el jugador pulsa el botón de disparo (Click izquierdo por defecto)
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        // Reproducir efecto de disparo y sonido
        if (efectoDisparo != null) efectoDisparo.Play();
        if (sonidoDisparo != null) sonidoDisparo.Play();

        // Crear un Raycast desde la posición del arma hacia adelante
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rangoDisparo))
        {
            // Comprobar si el objeto impactado tiene el script "Enemigo"
            Enemigo enemigo = hit.collider.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                // Reducir la vida del enemigo
                enemigo.RecibirDaño(daño);
            }

            Debug.Log($"Impacto en: {hit.collider.name}");
        }
    }
}
