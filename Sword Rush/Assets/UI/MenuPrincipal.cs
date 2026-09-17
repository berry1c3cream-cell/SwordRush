using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class MenuPrincipal : MonoBehaviour
{
    private UIDocument ui;
    //public UIDocument uiV2;
    private Button boton;
    private Button salir;
    public PVidaPlayer player;

    private void Awake()
    {
        ui = GetComponent<UIDocument>();

        boton = ui.rootVisualElement.Q<Button>("StartGameButton");
        salir = ui.rootVisualElement.Q<Button>("LeaveButton");

        // Buscar barra de vida
        //barraVida = uiV2.rootVisualElement.Q<ProgressBar>("GameUI");

        //if (barraVida == null)
        //{
        //    Debug.LogError("NO SE ENCONTRÓ BarraVida");
        //}
        //else
        //{
        //    // Ocultarla al comenzar
        //    barraVida.style.display = DisplayStyle.None;
        //}

        if (boton == null)
        {
            Debug.LogError("No se encontró StartGameButton.");
            return;
        }

        boton.RegisterCallback<ClickEvent>(OnPlayGameClick);

        if (salir == null)
        {
            Debug.LogError("No se encontró LeaveButton.");
            return;
        }

        salir.RegisterCallback<ClickEvent>(OnSalirClick);
    }

    private void OnDisable()
    {
        if (boton != null)
            boton.UnregisterCallback<ClickEvent>(OnPlayGameClick);

        if (salir != null)
            salir.UnregisterCallback<ClickEvent>(OnSalirClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Presionaste el boton");

        PMovimiento movimiento = FindFirstObjectByType<PMovimiento>();
        PEspada espada = FindFirstObjectByType<PEspada>();
        PVidaPlayer vidaPlayer = FindFirstObjectByType<PVidaPlayer>();

        if (movimiento != null)
            movimiento.IniciarJuego();

        if (espada != null)
            espada.IniciarJuego();

        if (vidaPlayer != null)
            vidaPlayer.MostrarBarraVida();

        // Ocultar solamente el menú
        ui.rootVisualElement.style.display = DisplayStyle.None;

        player.MostrarBarraVida();
    }

    private void OnSalirClick(ClickEvent evt)
    {
        Application.Quit();
    }
}