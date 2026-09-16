using UnityEngine;

public class PObjetoHabilidad : MonoBehaviour
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
            PHabilidades habilidades = other.GetComponent<PHabilidades>();

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
