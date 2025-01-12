using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{

    public float rangoDisparo = 50f; // Rango m�ximo del arma
    public int da�o = 20; // Da�o que inflige el arma
    public ParticleSystem efectoDisparo; // Efecto visual del disparo (opcional)
    public AudioSource sonidoDisparo; // Sonido del disparo (opcional)

    void Update()
    {
        // Detectar si el jugador pulsa el bot�n de disparo (Click izquierdo por defecto)
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

        // Crear un Raycast desde la posici�n del arma hacia adelante
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rangoDisparo))
        {
            // Comprobar si el objeto impactado tiene el script "Enemigo"
            Enemigo enemigo = hit.collider.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                // Reducir la vida del enemigo
                enemigo.RecibirDa�o(da�o);
            }

            Debug.Log($"Impacto en: {hit.collider.name}");
        }
    }
}
