using UnityEngine;

public class PEspada : MonoBehaviour
{
    public int daño = 1;
    public float distanciaAtaque = 3f;
    public float velocidadAtaque = 10f;
    public Camera camara;

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

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            rotacionAtaque,
            velocidadAtaque * Time.deltaTime
        );

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

            PVidaEnemigo vida = golpe.collider.GetComponentInParent<PVidaEnemigo>();

            if (vida != null)
            {
                vida.RecibirDaño(daño);
                yaGolpeo = true;

                Debug.Log("¡Espada golpeó al enemigo!");
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
