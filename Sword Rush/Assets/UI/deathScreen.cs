using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class deathScreen : MonoBehaviour
{
    private UIDocument ui;
    private VisualElement pantallaMuerte;
    private Button reiniciar;
    private Button salir;
    public PVidaPlayer player;

    private void Awake()
    {
        ui = GetComponent<UIDocument>();
        reiniciar = ui.rootVisualElement.Q<Button>("ReiniciarButton");
        salir = ui.rootVisualElement.Q<Button>("SalirButton");

        if (reiniciar == null)
        {
            Debug.LogError("No se encontró el boton de reiniciar.");
            return;
        }

        reiniciar.RegisterCallback<ClickEvent>(OnReiniciarGameClick);

        if (salir == null)
        {
            Debug.LogError("No se encontró salir.");
            return;
        }

        salir.RegisterCallback<ClickEvent>(OnSalirClick);

        pantallaMuerte = ui.rootVisualElement.Q("DeathScreen");

        if (pantallaMuerte == null)
        {
            Debug.LogError("No se encontró el elemento 'DeathScreen'. Revisa el Name en UI Builder.");
            return;
        }

        // Ocultar la pantalla de muerte al comenzar
        pantallaMuerte.style.display = DisplayStyle.None;
    }

    public void MostrarPantallaMuerte()
    {
        player.ocultarBarraVida();

        if (pantallaMuerte != null)
        {
            pantallaMuerte.style.display = DisplayStyle.Flex;

            // Cursor visible mientras está el menú
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }

    private void OnReiniciarGameClick(ClickEvent evt)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSalirClick(ClickEvent evt)
    {
        Application.Quit();
    }
}


