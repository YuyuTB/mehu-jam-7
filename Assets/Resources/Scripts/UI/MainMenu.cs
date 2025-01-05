using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    private Button _startGameButton;
    private Button _controllerInputButton;
    private Button _keyboardInputButton;
    
    private enum InputType { Controller, Keyboard }
    
    void Start()
    {
        _startGameButton = transform.Find("StartGame").GetComponent<Button>();
        _controllerInputButton = transform.Find("InputGamepad").GetComponent<Button>();
        _keyboardInputButton = transform.Find("InputMNK").GetComponent<Button>();

        // Set the initial selected button for gamepad navigation
        EventSystem.current.SetSelectedGameObject(_controllerInputButton.gameObject);
        
        // Change the color of the disabled state
        ColorBlock cb = _startGameButton.colors;
        cb.disabledColor = Color.gray;
        
        // Set the desired color
        _startGameButton.colors = cb;
        _startGameButton.interactable = false;
        
        _startGameButton.onClick.AddListener(StartGame);
        _controllerInputButton.onClick.AddListener(() => ChooseInput(InputType.Controller));
        _keyboardInputButton.onClick.AddListener(() => ChooseInput(InputType.Keyboard));
        
        // Set the initial selected button for gamepad navigation
        EventSystem.current.SetSelectedGameObject(_controllerInputButton.gameObject);
    }
    
    
    void Update()
    {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
        
        if (selectedButton == null)
        {
            return;
        }
        
        HandleGamepadInput(selectedButton.GetComponent<Button>());
    }
    
    private void EnableButton(Button button)
    {
        button.interactable = true;
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
    
    private void ChooseInput(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Controller:
                EnableButton(_startGameButton);
                SaveInputType("Controller");
                break;
            case InputType.Keyboard:
                EnableButton(_startGameButton);
                SaveInputType("Keyboard");
                break;
        }
    }
    
    private void SaveInputType(string inputType)
    {
        PlayerPrefs.SetString("InputType", inputType);
        PlayerPrefs.Save();
    }
    
    private void StartGame()
    {
        // Remove the listeners to prevent memory leaks
        _startGameButton.onClick.RemoveListener(StartGame);
        _controllerInputButton.onClick.RemoveListener(() => ChooseInput(InputType.Controller));
        _keyboardInputButton.onClick.RemoveListener(() => ChooseInput(InputType.Keyboard));
        
        SceneManager.LoadScene("GameScene");
    }
}