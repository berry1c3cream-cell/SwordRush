using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class victory : MonoBehaviour
{
    private UIDocument ui;
    private VisualElement pantallaWin;
    private Button reiniciar;
    private Button salir;
    public PVidaPlayer player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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

        pantallaWin = ui.rootVisualElement.Q("WinScreen");

        if (pantallaWin == null)
        {
            Debug.LogError("No se encontró el elemento 'WinScreen'. Revisa el Name en UI Builder.");
            return;
        }

        // Ocultar la pantalla de muerte al comenzar
        pantallaWin.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MostrarPantallaWIN()
    {
        if (pantallaWin != null)
        {
            player.ocultarBarraVida();

            pantallaWin.style.display = DisplayStyle.Flex;

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
