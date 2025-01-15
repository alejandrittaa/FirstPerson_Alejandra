using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarArmaEscudo : MonoBehaviour
{
    public GameObject arma; // Asigna el objeto del arma en el Inspector
    public GameObject escudo; // Asigna el objeto del escudo en el Inspector
    private bool tieneEscudo = false; // Estado actual del escudo

    void Update()
    {
        // Cambiar al escudo (tecla 2)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivarEscudo();
        }

        // Cambiar al arma (tecla 1)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivarArma();
        }
    }

    private void ActivarEscudo()
    {
        if (!tieneEscudo) // Solo cambiar si actualmente no está activo el escudo
        {
            arma.SetActive(false); // Desactivar el arma
            escudo.SetActive(true); // Activar el escudo
            tieneEscudo = true; // Actualizar estado
            Debug.Log("Escudo activado. Ahora eres inmune a ataques.");
        }
    }

    private void ActivarArma()
    {
        if (tieneEscudo) // Solo cambiar si el escudo está activo
        {
            escudo.SetActive(false); // Desactivar el escudo
            arma.SetActive(true); // Activar el arma
            tieneEscudo = false; // Actualizar estado
            Debug.Log("Arma activada. Ahora puedes atacar.");
        }
    }

    // Método para verificar si el jugador está usando el escudo
    public bool EstaUsandoEscudo()
    {
        return tieneEscudo;
    }
}
