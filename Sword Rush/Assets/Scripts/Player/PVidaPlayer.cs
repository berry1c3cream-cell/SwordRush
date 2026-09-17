using UnityEngine;
using UnityEngine.UIElements;

public class PVidaPlayer : MonoBehaviour
{
    public int vida = 100;
    public int vidaMaxima = 100;

    private bool muerto = false;

    public deathScreen pantalla;

    public UIDocument hud;

    private ProgressBar barraVida;
    public VisualElement UiPantalla;

    void Start()
    {
        barraVida = hud.rootVisualElement.Q<ProgressBar>("HealthBar");

        if (barraVida != null)
        {
            barraVida.highValue = vidaMaxima;
            barraVida.value = vida;

            // Oculta al comenzar
            //barraVida.style.display = DisplayStyle.None;
            //UiPantalla.style.display = DisplayStyle.None;
            ocultarBarraVida();
        }
        else
        {
            Debug.LogError("No se encontró BarraVida en el HUD.");
        }
    }

    public void ocultarBarraVida()
    {
        UiPantalla = hud.rootVisualElement.Q<VisualElement>("BarraVida");

        if (UiPantalla != null)
        {
            UiPantalla.style.display = DisplayStyle.None;
        }
        else
        {
            Debug.LogError("No se encontró BarraVida en el HUD.");
        }
    }

    public void MostrarBarraVida()
    {
        UiPantalla.style.display = DisplayStyle.Flex;

        //if (barraVida != null)
        //{
        //    barraVida.style.display = DisplayStyle.Flex;
        //}
    }

    public void RecibirDaño(int daño)
    {
        if (muerto)
            return;

        vida -= daño;
        vida = Mathf.Max(vida, 0);

        if (barraVida != null)
            barraVida.value = vida;

        Debug.Log("Vida del jugador: " + vida);

        if (vida <= 0)
            Morir();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !muerto)
        {
            RecibirDaño(1);
        }
    }

    void Morir()
    {
        muerto = true;

        PMovimiento movimiento = GetComponent<PMovimiento>();

        if (movimiento != null)
            movimiento.enabled = false;

        pantalla.MostrarPantallaMuerte();
    }
}