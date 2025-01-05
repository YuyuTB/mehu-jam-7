using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
    private Button _mainMenuButton;
    private void Start()
    {
        _mainMenuButton = transform.Find("MainMenu").GetComponent<Button>();
        _mainMenuButton.onClick.AddListener(LoadMainMenu);
        
        // Set the initial selected button for gamepad navigation
        EventSystem.current.SetSelectedGameObject(_mainMenuButton.gameObject);
    }

    private void Update()
    {
        HandleGamepadInput(_mainMenuButton);
    }

    private void HandleGamepadInput(Button button)
    {
        // Check if the "Submit" button (usually the "A" button on a gamepad) is pressed
        if (Input.GetButtonDown("Submit"))
        {
            // Invoke the button's click event
            button.onClick.Invoke();
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
