using System;
using UnityEngine;

public class DisplayHUDForSelectedInput : MonoBehaviour
{
    private string _inputSelected;
    private string _chosenHUDPath;

    private void Start()
    {
        GetPlayerPreferences();
        
        if (string.IsNullOrEmpty(_inputSelected))
        {
            return;
        }

        ChooseHUD();
        DisplayHUD();
    }
    
    private void DisplayHUD()
    {
        GameObject hud = Resources.Load<GameObject>(_chosenHUDPath);
        Instantiate(hud, transform);
    }
    
    private void ChooseHUD()
    {
        string basePath = "Prefabs/HUD/";
        
        if (_inputSelected == "Controller")
        {
            _chosenHUDPath = basePath + "UI-Controller";
        }
        else if (_inputSelected == "Keyboard")
        {
            _chosenHUDPath = basePath + "UI-MNK";
        }
    }

    private void GetPlayerPreferences()
    {
        _inputSelected = PlayerPrefs.GetString("InputType");
    }
}
