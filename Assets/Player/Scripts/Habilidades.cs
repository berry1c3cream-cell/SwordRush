using UnityEngine;

public class Habilidades : MonoBehaviour
{
    public bool habilidadFDesbloqueada = false;
    public bool habilidadGDesbloqueada = false;

    public bool habilidadFActiva = false;
    public bool habilidadGActiva = false;

    public float duracionHabilidad = 5f;
    public float tiempoRecarga = 10f;

    private float tiempoF = 0f;
    private float recargaF = 0f;

    private float tiempoG = 0f;
    private float recargaG = 0f;

    void Update()
    {
        // Habilidad F
        if (habilidadFDesbloqueada)
        {
            if (recargaF > 0)
                recargaF -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.F) && recargaF <= 0 && !habilidadFActiva)
            {
                ActivarF();
            }

            if (habilidadFActiva)
            {
                tiempoF -= Time.deltaTime;

                if (tiempoF <= 0)
                {
                    DesactivarF();
                }
            }
        }

        // Habilidad G
        if (habilidadGDesbloqueada)
        {
            if (recargaG > 0)
                recargaG -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.G) && recargaG <= 0 && !habilidadGActiva)
            {
                ActivarG();
            }

            if (habilidadGActiva)
            {
                tiempoG -= Time.deltaTime;

                if (tiempoG <= 0)
                {
                    DesactivarG();
                }
            }
        }
    }

    void ActivarF()
    {
        habilidadFActiva = true;
        tiempoF = duracionHabilidad;

        Debug.Log("F activada: ataque rápido");
    }

    void DesactivarF()
    {
        habilidadFActiva = false;
        recargaF = tiempoRecarga;

        Debug.Log("F terminada. Recarga: 10 segundos");
    }

    void ActivarG()
    {
        habilidadGActiva = true;
        tiempoG = duracionHabilidad;

        Debug.Log("G activada: 5 de daño");
    }

    void DesactivarG()
    {
        habilidadGActiva = false;
        recargaG = tiempoRecarga;

        Debug.Log("G terminada. Recarga: 10 segundos");
    }

    public bool FDisponible()
    {
        return habilidadFDesbloqueada && !habilidadFActiva && recargaF <= 0;
    }

    public bool GDisponible()
    {
        return habilidadGDesbloqueada && !habilidadGActiva && recargaG <= 0;
    }
}