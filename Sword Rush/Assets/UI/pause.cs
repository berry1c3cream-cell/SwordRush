using UnityEngine;
using UnityEngine.UIElements;

public class pause : MonoBehaviour
{
    private UIDocument ui;
    private Button salir;
    public PVidaPlayer player;
    public Button continuar;
    public GameObject screen;
    public GameObject mainUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screen.SetActive(false);
    }

    private void Awake()
    {
        ui = GetComponent<UIDocument>();

        Debug.Log("PAUSE UIDOCUMENT: " + ui);

        ui.rootVisualElement.RegisterCallback<PointerDownEvent>(evt =>
        {
            Debug.Log("¡¡¡EL PAUSE UI RECIBIÓ POINTER DOWN!!!");
        });

        salir = ui.rootVisualElement.Q<Button>("LeaveButton");
        continuar = ui.rootVisualElement.Q<Button>("continuarBotton");

        Debug.Log("BOTÓN CONTINUAR: " + continuar);

        if (salir == null)
        {
            Debug.LogError("NO se encontró LeaveButton.");
        }

        if (continuar == null)
        {
            Debug.LogError("NO se encontró continuarButton.");
        }

        if (salir != null)
            salir.RegisterCallback<ClickEvent>(OnSalirClick);

        if (continuar != null)
        {
            continuar.RegisterCallback<ClickEvent>(OnContinuarClick);

            continuar.RegisterCallback<PointerDownEvent>(evt =>
            {
                Debug.Log("¡¡¡POINTER DOWN EN CONTINUAR!!!");
            });
        }
          
    }

    private void OnDisable()
    {

        if (salir != null)
            salir.UnregisterCallback<ClickEvent>(OnSalirClick);
    }

    private void OnSalirClick(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnContinuarClick(ClickEvent evt)
    {
        Debug.Log("Presionaste el boton");
        Time.timeScale = 1f;

        screen.SetActive(false);
        mainUI.SetActive(true);

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (screen != null && screen.activeSelf)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }
}
