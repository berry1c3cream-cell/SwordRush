using UnityEngine;

public class ObjetoHabilidad : MonoBehaviour
{
    public enum TipoHabilidad
    {
        F,
        G
    }

    public TipoHabilidad habilidad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Habilidades habilidades = other.GetComponent<Habilidades>();

            if (habilidades != null)
            {
                if (habilidad == TipoHabilidad.F)
                {
                    habilidades.habilidadFDesbloqueada = true;
                    Debug.Log("¡Habilidad F desbloqueada!");
                }

                if (habilidad == TipoHabilidad.G)
                {
                    habilidades.habilidadGDesbloqueada = true;
                    Debug.Log("¡Habilidad G desbloqueada!");
                }

                Destroy(gameObject);
            }
        }
    }
}