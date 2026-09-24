using UnityEngine;
using UnityEngine.UIElements;

public class gameUi : MonoBehaviour
{
    public GameObject screen;
    public GameObject mainUI;
    private UIDocument ui;
    private Button pausa;

    private void Awake()
    {
        ui = GetComponent<UIDocument>();

        pausa = ui.rootVisualElement.Q<Button>("pauseButton");
    }

    private void Start()
    {
        pausa.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Time.timeScale = 0f;

            screen.SetActive(true);
            mainUI.SetActive(false);

            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }
}