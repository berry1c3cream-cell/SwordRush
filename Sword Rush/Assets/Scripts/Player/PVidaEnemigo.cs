using UnityEngine;
using System.Collections;

public class PVidaEnemigo : MonoBehaviour
{
    public int vida = 3;

    private Renderer[] renderers;
    private Color[] coloresOriginales;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();

        coloresOriginales = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            coloresOriginales[i] = renderers[i].material.color;
        }
    }

    public void RecibirDaño(int daño)
    {
        vida -= daño;

        Debug.Log("Vida del enemigo: " + vida);

        StartCoroutine(DañoRojo());

        if (vida <= 0)
        {
            Debug.Log("Enemigo muerto");
            Destroy(gameObject);
        }
    }

    IEnumerator DañoRojo()
    {
        // Poner todo el modelo rojo
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = Color.red;
        }

        // Duración del flash
        yield return new WaitForSeconds(0.08f);

        // Regresar a los colores originales
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = coloresOriginales[i];
        }
    }
}