using UnityEngine;

public class Espada : MonoBehaviour
{
    public int daño = 1;
    public float distanciaAtaque = 3f;
    public float velocidadAtaque = 10f;

    public Camera camara;

    private Habilidades habilidades;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    private bool atacando = false;
    private bool regresando = false;
    private bool yaGolpeo = false;

    void Start()
    {
        posicionInicial = transform.localPosition;
        rotacionInicial = transform.localRotation;

        if (camara == null)
        {
            camara = Camera.main;
        }

        habilidades = GetComponentInParent<Habilidades>();

        if (habilidades == null)
        {
            Debug.LogError("No se encontró Habilidades en el Player.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !atacando && !regresando)
        {
            atacando = true;
            yaGolpeo = false;
        }

        if (atacando)
        {
            Ataque();
        }

        if (regresando)
        {
            Regresar();
        }
    }

    void Ataque()
    {
        Quaternion rotacionAtaque = Quaternion.Euler(80f, 0f, 0f);

        float velocidadActual = velocidadAtaque;

        // Habilidad F: ataque más rápido
        if (habilidades != null && habilidades.habilidadFActiva)
        {
            velocidadActual = velocidadAtaque * 4f;
        }

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            rotacionAtaque,
            velocidadActual * Time.deltaTime
        );

        // Detectar enemigo una sola vez
        if (!yaGolpeo)
        {
            DetectarEnemigo();
        }

        if (Quaternion.Angle(transform.localRotation, rotacionAtaque) < 1f)
        {
            atacando = false;
            regresando = true;
        }
    }

    void DetectarEnemigo()
    {
        RaycastHit golpe;

        Ray rayo = camara.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f, 0)
        );

        if (Physics.Raycast(rayo, out golpe, distanciaAtaque))
        {
            Debug.Log("Golpeaste: " + golpe.collider.gameObject.name);

            VidaEnemigo vida = golpe.collider.GetComponentInParent<VidaEnemigo>();

            if (vida != null)
            {
                int dañoActual = daño;

                // Habilidad G: 3 de daño
                if (habilidades != null && habilidades.habilidadGActiva)
                {
                    dañoActual = 4;
                }

                vida.RecibirDaño(dañoActual);

                yaGolpeo = true;

                Debug.Log("¡Espada golpeó al enemigo!");
                Debug.Log("Daño realizado: " + dañoActual);
            }
        }
        else
        {
            Debug.Log("No golpeaste nada");
        }
    }

    void Regresar()
    {
        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            rotacionInicial,
            velocidadAtaque * Time.deltaTime
        );

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            posicionInicial,
            velocidadAtaque * Time.deltaTime
        );

        if (Quaternion.Angle(transform.localRotation, rotacionInicial) < 1f)
        {
            transform.localRotation = rotacionInicial;
            transform.localPosition = posicionInicial;

            regresando = false;
        }
    }
}